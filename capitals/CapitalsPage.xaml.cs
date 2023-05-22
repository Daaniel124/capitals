using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace capitals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CapitalsPage : ContentPage
    {
        private Entry ent;
        private Button btn1, btn2, btn3;
        private Label lbl;

        public const string fileName = "capitals1.txt";
        public CapitalsPage()
        {
            ent = new Entry
            {
                Placeholder = "Введите столицу"
            };

            btn1 = new Button
            {
                Text = "Добавить",
                HorizontalOptions = LayoutOptions.Center
            };
            btn1.Clicked += Btn1_Clicked; ;


            btn2 = new Button
            {
                Text = "Показать столицы",
                HorizontalOptions = LayoutOptions.Center
            };
            btn2.Clicked += Btn2_Clicked; ;

            lbl = new Label
            {
                Text = "Столицы будут показываться здесь",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            btn3 = new Button
            {
                Text = "Начать",
                HorizontalOptions = LayoutOptions.Center
            };

            btn3.Clicked += Btn3_Clicked; ;

            Content = new StackLayout
            {
                Children = { ent, btn1, btn2, btn3, lbl }
            };

            CreateDictionaryFile();
        }

        private void Btn3_Clicked(object sender, EventArgs e)
        {
            string[] capitalsList = ReadWordsFromFile();

            if (capitalsList != null && capitalsList.Length >= 2)
            {
                string wordOutput = string.Empty;

                Random random = new Random();

                string randomWord = capitalsList[random.Next(capitalsList.Length)];

                lbl.Text = wordOutput + randomWord;
            }
            else
            {
                lbl.Text = "Файл со столицами пуст";
            }
            
        }

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            string[] capitalsList = ReadWordsFromFile();

            if (capitalsList != null && capitalsList.Length >= 2)
            {
                string wordOutput = string.Empty;

                foreach (string word in capitalsList)
                {
                    wordOutput += word + "\n";
                }

                lbl.Text = wordOutput;

            }
            else
            {
                lbl.Text = "Не удалось извлечь столицы из файла";
            }
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            string word = ent.Text;

            if (!string.IsNullOrWhiteSpace(word))
            {
                lbl.Text = $"Столица: {word}\n";

                SaveCapitalsToFile(word);
            }
            else
            {
                lbl.Text = "Пожалуйста, введите обе столицы";
            }

            ent.Text = string.Empty;
        }

        private void SaveCapitalsToFile(string word2)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            string content = $"{word2}\n";

            using (StreamWriter write = File.AppendText(filePath))
            {
                write.WriteLine(word2);

            }
        }
        private void CreateDictionaryFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            if (!File.Exists(filePath))
            {
                string content = string.Empty;
                File.WriteAllText(filePath, content);
            }
        }

        public static string[] ReadWordsFromFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                string[] words = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                return words;
            }

            return null;
        }
    }
}