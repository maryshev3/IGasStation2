using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.Utils
{
    public static class Constants
    {
        public const string AllowedPower = "Разрешённой мощности";
        public const string CurrentPower = "Установленной мощности";
        public const string PowerUsagePerYear = "Энергопотребления за год";

        public const string SetIsNormally = "Выборка является нормально распределённой";
        public const string SetIsNotNormally = "Выборка не является нормально распределённой";
        public const string SetIsNotNormallyNeedAppend = "Выборка не является нормально распределённой. Для нормализации необходимо добавить данные";
        public const string SetIsNotChecked = "Выборка не проверялась на нормальность распределения";

        public const string DeleteToNormallyWillHelp = "Удаление следующих записей из БД поможет нормализовать её";
        public const string DeleteToNormallyWillNotHelp = "Удаление следующих записей из БД не поможет нормализовать её";
    }
}
