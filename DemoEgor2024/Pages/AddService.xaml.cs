using DemoEgor2024.database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoEgor2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddService.xaml
    /// </summary>
    public partial class AddService : Page
    {
        private string selectedImagePath;
        public string folderName = "Услуги школы";
        int charactersToRemove = 61; // Количество символов для удаления в начале строки
        public AddService()
        {
            InitializeComponent();
            //var imagesBD = App.db.ServicePhoto.FirstOrDefault(x => x.ID == ser.ServicePhotoID).PhotoPath.ToString();
            //string folderName = "DemoEgor2024/Resource";
            //string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //string fullPath = System.IO.Path.Combine(projectDirectory, folderName, imagesBD);

            ////Заменяем обратные слеши на прямые слеши
            //ImageService.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            //TitleServiceTBox.Text = ser.Title.ToString();
            //CostTBox.Text = ser.Cost.ToString();
            //TimeTBox.Text = ser.DurationInMinutes.ToString();
            //DiscountTBox.Text = ser.Discount.ToString();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.EnterPage());
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Service ser = new Service();
            ser.Title = TitleServiceTBox.Text;
            ser.Cost = Convert.ToDecimal(CostTBox.Text);
            ser.DurationInMinutes = Convert.ToInt32(TimeTBox.Text);
            ser.Discount = Convert.ToInt32(DiscountTBox.Text);
            ser.ServicePhotoID = App.db.ServicePhoto.FirstOrDefault(x => x.PhotoPath == selectedImagePath).ID;
            App.db.Service.Add(ser);
            App.db.SaveChanges();
            NavigationService.Navigate(new Pages.EnterPage());
        }
        private void Button_Click_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {

                selectedImagePath = openFileDialog.FileName;

                // Ищем индекс папки "Услуги школы" и обрезаем строку
                int index = selectedImagePath.IndexOf(folderName);

                if (index != -1)
                {
                    string result = selectedImagePath.Substring(index);
                    selectedImagePath = result;
                }
                ImageService.Source = new BitmapImage(new Uri(openFileDialog.FileName));

            }
        }

    }
}

