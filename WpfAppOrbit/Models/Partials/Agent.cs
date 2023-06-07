using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppOrbit.Models
{
    public partial class Agent
    {
        /// <summary>
        /// Возвращает абсолютный путь к изображению
        /// </summary>
        public string GetPhoto
        {
            get
            {
                if (Logo is null)
                    return Directory.GetCurrentDirectory() + @"\Images\picture.png";
                return Directory.GetCurrentDirectory() + @"\Images\" + Logo.Trim();
            }
        }
        /// <summary>
        /// Возвращает сумму продаж за весь период
        /// </summary>
        public double TotalSum
        {
            get
            {
                double sum = 0;
                foreach (SellHistory sell in SellHistories)
                {
                    sum += sell.Product.MinimalPrice * sell.Count;

                }
                return sum;
            }
        }

        /// <summary>
        /// Возвращает размер скидки
        /// </summary>
        public int Discount
        {
            get
            {
                if (TotalSum < 10000)
                    return 0;
                if (TotalSum < 50000)
                    return 5;
                if (TotalSum < 150000)
                    return 10;
                if (TotalSum < 500000)
                    return 20;
                return 25;
            }
        }

        /// <summary>
        /// Количество продаж за год
        /// </summary>
        public int SellCountInYear
        {
            get
            {
                int sum = 0;
                foreach (SellHistory sell in SellHistories)
                {
                    DateTime dateTimeToday = DateTime.Today;
                    DateTime dateTimeYearAgo = dateTimeToday.AddDays(-365);
                    if ((sell.Date > dateTimeYearAgo) && (sell.Date < dateTimeToday))
                        sum += sell.Count;

                }
                return sum;
            }
        }

        /// <summary>
        /// Количество продаж за год строка
        /// </summary>

        public string GetSellCountInYear
        {
            get
            {
                return $"{SellCountInYear} продаж за год";
            }
        }




        /// <summary>
        /// Задает цвет фона агента зеленый для агентов со скидкой более 25%
        /// </summary>
        public string GetColor
        {
            get
            {
                if (Discount >= 25)
                    return "#FF1DDE40";
                else
                    return "#fff";
            }
        }
        /// <summary>
        /// Поле будет использовано для отображения информации об агенте
        /// </summary>        
        public string GetInfo
        {
            get
            {
                return $"{AgentType.AgentTypeName} | {AgentName} ";
            }
        }


    }
}
