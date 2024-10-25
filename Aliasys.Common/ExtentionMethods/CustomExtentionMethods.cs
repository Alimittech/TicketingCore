using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Globalization;

namespace Aliasys.Common.ExtentionMethods
{
    public static class CustomExtentionMethods
    {
        //Clear Extra Space
        public static string ClearExtraSpace(this string str)
        {
            bool check = false;
            short count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsWhiteSpace(str, i))
                {
                    if (check)
                    {
                        count++;
                    }
                    check = true;
                }
                else
                {
                    check = false;
                }
            }
            for (int i = 1; i <= count - 1; i++)
            {
                str = str.Replace("  ", " ");
            }
            str = str.Trim();
            return str;
        }

        //Convert ShamsiDate to Miladi
        public static string ConvertShamsiToMiladi(this string date)
        {
            PersianDateTime pc = PersianDateTime.Parse(date);
            return pc.ToDateTime().ToShortDateString();
        }

        //Convert MiladiDate to Shamsi
        public static string ConvertMiladiToShamsi(this DateTime date)
        {
            PersianDateTime pc = new PersianDateTime(date);
            return pc.ToPersianDateString();
        }

        //Convert To Shamsi
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");
        }

        //Convert Number to English
        public static string ToEnglishNumber(this string strInput)
        {
            string[] output = strInput.Split("/");
            strInput = string.Concat(output[0], output[1], output[2]);
            string EnglishNumbers = "";
            for (int i = 0; i < strInput.Length; i++)
            {
                if (Char.IsDigit(strInput[i]))
                {
                    EnglishNumbers += char.GetNumericValue(strInput, i);
                }
                else
                {
                    EnglishNumbers += (strInput[i].ToString());
                }
            }
            EnglishNumbers = EnglishNumbers.Insert(4, "/");
            EnglishNumbers = EnglishNumbers.Insert(7, "/");
            return EnglishNumbers;
        }

        //Separate 3 Digits
        public static string SeparateThreeDigits(this long value)
        {
            return value.ToString("#,0");
        }

        //Add 0 Digit Before Number
        public static string AddZeroDigitToNumber(this long value)
        {
            return value.ToString("000000");
        }

        //Check Ajax Request
        public static ResultDto IsAjaxRequest(this HttpRequest request, string httpVerb = "")
        {
            if (request != null)
            {
                if (!string.IsNullOrEmpty(httpVerb))
                {
                    if (request.Method.ToUpper() != httpVerb.ToUpper())
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            ActionType = ActionType.Failed,
                            Message = "HttpVerb Does Not Match"
                        };
                    }
                    if (request.Headers != null)
                    {
                        if (request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        {
                            return new ResultDto
                            {
                                IsSuccess = true,
                                ActionType = ActionType.Completed
                            };
                        }
                    }
                }
            }
            return new ResultDto
            {
                IsSuccess = false,
                ActionType = ActionType.Failed
            };
        }

        //Set Session
        public static void SetSessionModelData(this ISession session, string key, string value)
        {
            session.SetString(key, value);
        }

        //Get Session
        public static string GetSessionModelData(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : value;
        }

        //difference DataTime
        public static double DiffDateTime(this DateTime primaryDateTime, DateTime SecondaryDateTime)
        {
            return primaryDateTime.Subtract(SecondaryDateTime).TotalMinutes;
        }

        //Generate RandomCode
        public static int GenerateRandomCode(this int startNum, int endNum)
        {
            Random random = new Random();
            return random.Next(startNum, endNum);
        }

        //DbContext Check All Depenedency
        #region CheckDbContextDependency
        public static List<TEntity> GetAllDependentEntities<TEntity>(this DbContext ctx, TEntity entity)
          where TEntity : class
        {
            return ctx.GetAllRelatedEntities(entity, IsRelationshipParent);
        }

        public static List<TEntity> GetAllEntitiesDependingOn<TEntity>(this DbContext ctx, TEntity entity)
          where TEntity : class
        {
            return ctx.GetAllRelatedEntities(entity, IsRelationshipChild);
        }

        private static List<TEntity> GetAllRelatedEntities<TEntity>(this DbContext ctx, TEntity entity, Func<IRelatedEnd, bool> relationshipFilter)
          where TEntity : class
        {
            var result = new List<TEntity>();

            var queue = new Queue<TEntity>();
            queue.Enqueue(entity);

            while (queue.Any())
            {
                var current = queue.Dequeue();

                var foundDependencies = ctx.GetRelatedEntitiesFrom<TEntity>(current, relationshipFilter);
                foreach (var dependency in foundDependencies)
                {
                    if (!result.Contains(dependency))
                        queue.Enqueue(dependency);
                }

                result.Add(current);
            }
            return result;
        }

        private static List<TEntity> GetRelatedEntitiesFrom<TEntity>(this DbContext ctx, object entity, Func<IRelatedEnd, bool> relationshipFilter)
          where TEntity : class
        {
            var stateManager = (ctx as IObjectContextAdapter)?.ObjectContext?.ObjectStateManager;

            if (stateManager == null)
                return new List<TEntity>();

            if (!stateManager.TryGetRelationshipManager(entity, out var relationshipManager))
                return new List<TEntity>();

            return relationshipManager.GetAllRelatedEnds()
                                      .Where(relationshipFilter)
                                      .SelectMany(ExtractValues<TEntity>)
                                      .Where(x => x != null)
                                      .ToList();
        }

        private static IEnumerable<TEntity> ExtractValues<TEntity>(IRelatedEnd relatedEnd)
          where TEntity : class
        {
            if (!relatedEnd.IsLoaded)
                relatedEnd.Load();

            if (relatedEnd is IEnumerable enumerable)
                return ExtractCollection<TEntity>(enumerable);
            else
                return ExtractSingle<TEntity>(relatedEnd);
        }

        private static IEnumerable<TEntity> ExtractSingle<TEntity>(IRelatedEnd relatedEnd)
          where TEntity : class
        {
            var valueProp = relatedEnd.GetType().GetProperty("Value");
            var value = valueProp?.GetValue(relatedEnd);

            yield return value as TEntity;
        }

        private static IEnumerable<TEntity> ExtractCollection<TEntity>(IEnumerable enumerable)
        {
            return enumerable.OfType<TEntity>();
        }

        private static bool IsRelationshipParent(IRelatedEnd relatedEnd)
          => relatedEnd.SourceRoleName.Contains("Target");

        private static bool IsRelationshipChild(IRelatedEnd relatedEnd)
          => relatedEnd.TargetRoleName.Contains("Target");
        #endregion

    }
}
