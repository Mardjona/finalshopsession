using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalshopsession
{
    internal static class Stats //Статические данные
    {
        public static List<Product> _LboxItems = [ 
            new Product
                (   "Хлеб", 
                    0, 
                    3,
                    "Хлеб всему голова ", 
                    "ООО Хлебушек", 
                    null, 
                    45, 
                    25,
                    6
                ),
            new Product
                (  "По желанию",
                    1,
                    2,
                    "Желание - это жизнь на заимствованной энергии, которая никогда не остаётся с вами. Вы не можете поймать радугу, на санскрите радуга и желание обозначаются одним и тем же словом.", 
                    "ООО если есть желание",
                     null ,
                    4678,
                    3, 
                    0)
            ,
            new Product("Лада приора",
                7, 
                0,
                " С самого начала производства LADA Priora соответствовала всем предъявляемым экологическим нормам: Евро-3 для российского рынка и Евро-5 для рынка. После ужесточения экологических норм на российском рынке автомобили с 2011 года имеют электронную педаль газа и полностью соответствуют нормам Евро-4. ",
                "ООО машинки",
                null,
                90500,
                1,
                7),
            new Product("Ватные диски",
                4,
                4,
                "Мы – неизменные жители ватных комнат – ватные диски и ватные палочки. Главный наш минут в том, что мы одноразовые и не пригодны для переработки!",
                "Sumsung galaxy disk",
                null,
                169,
                45,
                0
                )
        ];




        public static List<Product> _FoundedProducts = []; //Список найденнных товаров
        public static List<User> _Users = [new User("admin", "admin", true, false), new User("user", "user", false, false)]; //Список пользователей
        public static User _UserAutorized = null; //Авторизированный пользователь
        public static Product _SelectedProduct = null; //Выбранный для редактирования товар
        public static List<string> _Cathegories = [ "Еда", "Техника", "Одежда", "Дом","Красота"]; //категории товаров
        public static List<string> _Measurements = ["шт.", "мм", "см", "м", "мл", "л", "г", "кг"]; //единицы измерения

        //Статические поля для сохранения параметров поиска
        public static int _SelectedInex_Price = 0; //Индекс элемента выпадающего списка цены
        public static int _SelectedInex_Supplier = 0; //Индекс элемента выпадающего списка производителей
        public static string _SearchingText = ""; //Поисковая строка
        
    }
}
