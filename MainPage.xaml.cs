using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace lab3
{

    public class Product : BindableObject
    //class Product Представляет один продукт
    //BindableObject Класс Предоставляет механизм, с помощью
    //которого разработчики приложений могут распространять изменения,
    //вносимые в данные в одном объекте, на другой объект, применяя проверку,
    //приведение типов и систему событий
    {
  
        private string _name;
        public string Name
        {
            get => _name; //получем имя
            set
            {
                if (_name == value) return; // если условие истинно то возвращаем
                _name = value; //присваиваем переменной значение
                OnPropertyChanged(nameof(Name));
                // метод OnPropertyChanged для вызова события
                // название будет использоваться в качестве параметра
            }
        }

        int _quantity;//переменная количества
        public int Quantity
        {
            get => _quantity; //получаем количество
            set
            {
                if (_quantity == value) return;
                // если условие истинно то возвращаемся
                _quantity = value;// присваиваем переменной значение
                OnPropertyChanged(nameof(Quantity));
                // метод OnPropertyChanged для вызова события
                // название будет использоваться в качестве параметра
            }
        }

        private Color _clr;
        public Color Clr
        {
            get => _clr; //получем цвет
            set
            {
                if (_clr == value) return;
                // если условие истинно то возвращаем
                _clr = value; // присваиваем переменной значение
                OnPropertyChanged(nameof(Clr));
                // метод OnPropertyChanged для вызова события
                // название будет использоваться в качестве параметра
            }
        }

        private bool _checked;
        public bool Checked
        {
            get => _checked; //получаем выполненно или нет
            set
            {
                //if (_checked == value) return;// если условие истинно то возвращаем
                _checked = value;// присваиваем переменной значение
                OnPropertyChanged(nameof(Checked));
                // метод OnPropertyChanged для вызова события
                // название будет использоваться в качестве параметра
            }
        }

    }

    public class ViewModel : BindableObject
    {
        //ViewModel – абстрактный класс, упрощающий реализацию паттерна MVVM в Android-приложении
        //BindableObject Класс Предоставляет механизм, с помощью
        //которого разработчики приложений могут распространять изменения,
        //вносимые в данные в одном объекте, на другой объект, применяя проверку,
        //приведение типов и систему событий
        public ViewModel()
        {
            //класс Comand Представляет команду, доступную для использования в контексте родительского
            //элемента CommandCollection.
            AddCommand = new Command<string>(ItemName =>
            //создаем новую команду строкового типа с параметром ItemName и присваиваем переменной
            {
                var Product = new Product();
                //создаем переменную типа var и присваемаем ей объект типа класса Product 
                Product.Name = ItemName;
                //Объекту Name из класса Product присваиваем значение параметра ItemName
                Product.Quantity = Convert.ToInt32(Quantity);
                //Объекту Quantity из класса Product присваиваем конвертированное значение 
                Product.Clr = Clr;
                //Объекту Clr из класса Product присваиваем значение Clr
                Products.Add(Product);
                //Вызываем команду Add с параметром Product
            }, x => string.IsNullOrWhiteSpace(x) == false);
            // IsNullOrWhiteSpace Указывает, имеет ли указанная строка значение null, является ли она пустой
            // строкой или строкой, состоящей только из символов-разделителей

            ChangeColor = new Command<Color>(newClr =>
            //создаем новую команду  типа Цвет с параметром newClr и присваиваем переменной
            {
                Clr = newClr;
                //присваиваем переменной значение
                }, x => x != Clr);

            Products = new ObservableCollection<Product>();
            //Присваиваем переменной новый объект класса ObservableCollection
            //ObservableCollection Представляет динамическую коллекцию данных, которая предоставляет уведомления
            //о добавлении или удалении элементов или обновлении всего списка

            DelChecked = new Command(() =>
            //Создаем новую команду и присваиваем переменной
            {
                foreach (var product in Products.ToList())
                {       
                    if (product.Checked == true) //если условие истинно
                    {
                        Products.Remove(product); //уничтожение если класс не пересоздается
                    }
                }
            }, () => true);
        }

        int _quantity; //создаем переменную
        public int Quantity
        {
            get => _quantity; //получаем количество
            set
            {
                if (_quantity == value) return;
                // если условие истинно то возвращаемся
                _quantity = value;
                // присваиваем переменной значение
                OnPropertyChanged(nameof(Quantity));
                // метод OnPropertyChanged для вызова события
                // название будет использоваться в качестве параметра
            }
        }

        private Color _clr = Color.Gainsboro;
        //gainsboro цвета по шестнадцатеричному коду цвета #dcdcdc
        public Color Clr
        {
            get => _clr; //получем цвет
            set
            {
                if (_clr == value) return;
                // если условие истинно то возвращаем
                _clr = value;
                // присваиваем переменной значение
                OnPropertyChanged(nameof(Clr));
                // метод OnPropertyChanged для вызова события
                // название будет использоваться в качестве параметра
            }
        }





        public ObservableCollection<Product> Products { get; }
        //ObservableCollection Представляет динамическую коллекцию данных, которая предоставляет уведомления
        //о добавлении или удалении элементов или обновлении всего списка

        public ICommand
            AddCommand { get; }

        public ICommand
            ChangeColor { get; }

        public ICommand
            DelChecked { get; }


    }

    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel();
        }
        
    }
}
