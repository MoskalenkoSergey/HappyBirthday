using System;
using System.Globalization;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;

namespace WPF1MS43P
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Back.Visibility = Visibility.Collapsed;
        }

        private void Resultat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? HappyBirthday = DP.SelectedDate;
                if (!HappyBirthday.HasValue)
                {
                    MessageBox.Show("Выберите дату рождения");
                    return;
                }
                DateTime date = HappyBirthday.Value;
                if (date > DateTime.Now)
                {
                    Back.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Вы не могли родиться в будующем");
                }
                else
                {
                    ZnakCal.SelectedIndex = 0;
                    ResCal.Text = "";
                    ResCal1.Text = num1(date);
                    ResCal2.Text = num2(date);
                    ResCal3.Text = num3(date);
                    Back.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string num1(DateTime actualDate)
        {
            DateTime date = new DateTime(actualDate.Year + 1, 12, 31);
            double allDay = (DateTime.Now - actualDate).TotalDays + 1;
            int year = 0, month = 0, day = 0;
            while (allDay > date.DayOfYear)
            {
                year++;
                if (date.Year == DateTime.Now.Year) break;
                allDay -= date.DayOfYear;
                date = date.AddYears(1);
            }
            date = new DateTime(date.Year, 1, 1);
            while (allDay > DateTime.DaysInMonth(date.Year, date.Month))
            {
                allDay -= DateTime.DaysInMonth(date.Year, date.Month);
                month++;
                date = date.AddMonths(1);
            }
            if (DateTime.Now > new DateTime(DateTime.Now.Year, actualDate.Month, actualDate.Day) && month > 12)
            {
                month -= 12;
            }
            if (allDay > 0)
            {
                day = (int)allDay / 1;
            }
            string finish = $"Вам {year} лет {month} месяцев {day} дней";
            return finish;
        }

        private string num2(DateTime actualDate)
        {
            DateTime date = actualDate;
            int k = 0;
            while (date <= DateTime.Now)
            {
                if (actualDate.DayOfWeek == date.DayOfWeek)
                {
                    k++;
                }
                date = date.AddYears(1);
            }
            return $"День недели в который вы родились - {actualDate:dddd}\nКоличество дней рождения, которые выпадали на этот день недели - {k}";
        }

        private string num3(DateTime actualDate)
        {
            DateTime date = new DateTime(actualDate.Year, 12, 31);
            string value = "Високосные года: ";
            int k = 0;
            while (date.Year <= DateTime.Now.Year)
            {
                if (date.DayOfYear == 366)
                {
                    value += $"{date:yyyy}, ";
                    k++;
                }
                date = date.AddYears(1);
            }
            return k == 0 ? "Количество високосных лет со дня вашего рождения 0" : $"Количество високосных лет после вашего рождения - {k}\n{value.Remove(value.Length - 2)}";
        }

        private void Zodiak_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ZnakCal.SelectedIndex != 0)
                {
                    string Calendar = "";
                    switch (ZnakCal.SelectedIndex)
                    {
                        case 1:
                            Calendar = SlavCal();
                            break;
                        case 2:
                            Calendar = EastCal();
                            break;
                        default:
                            MessageBox.Show("Что-то пошло не по плану");
                            break;
                    }
                    ResCal.Text = Calendar;
                }
                else
                {
                    MessageBox.Show("Так не работает, нужно выберать календарь");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string EastCal()
        {
            string[] animals = { "обезьяна", "петух", "собака", "кабан", "крыса", "бык", "тигр", "кот", "дракон", "змея", "конь", "овца" };
            string[] colors = { "белый", "черный", "синий", "красный", "желтый" };
            DateTime date = DP.SelectedDate.Value;
            int animalIndex = date.Year % 12;
            int colorIndex = date.Year % 10 / 2;
            return $"Ваше животное - {animals[animalIndex]}, ваш цвет - {colors[colorIndex]}";
        }

        private string SlavCal()
        {
            DateTime date = DP.SelectedDate.Value;
            string res = "Вы - ";
            if ((date.Month == 12 && date.Day >= 24) || (date.Month == 1 && date.Day <= 30))
            {
                res += "Мороз";
            }
            else if (date.Month == 1 && date.Day >= 31 || (date.Month == 2 && date.Day <= 28))
            {
                res += "Велес";
            }
            else if (date.Month == 3 && date.Day >= 1 && date.Day <= 31)
            {
                res += "Макошь";
            }
            else if (date.Month == 4 && date.Day >= 1 && date.Day <= 30)
            {
                res  += "Жива";
            }
            else if (date.Month == 5 && date.Day >= 1 && date.Day <= 14)
            {
                res += "Ярила";
            }
            else if ((date.Month == 5 && date.Day >= 15) || (date.Month == 6 && date.Day <= 2))
            {
                res += "Леля";
            }
            else if (date.Month == 6 && date.Day >= 3 && date.Day <= 12)
            {
                res += "Кострома";
            }
            else if (date.Month == 6 && date.Day >= 13 && date.Day != 24 || (date.Month == 7 && date.Day <= 6 ))
            {
                res += "Додола";
            }
            else if (date.Month == 6 && date.Day == 24)
            {
                res += "Иван Купала";
            }
            else if (date.Month == 7 && date.Day >= 7 && date.Day <= 31)
            {
                res += "Лада";
            }
            else if (date.Month == 8 && date.Day >= 1 && date.Day <= 28)
            {
                res += "Перун";
            }
            else if ((date.Month == 8 && date.Day >= 29) || (date.Month == 9 && date.Day <= 13))
            {
                res += "Сева";
            }
            else if (date.Month == 9 && date.Day >= 14 && date.Day <= 27)
            {
                res += "Рожаница";
            }
            else if ((date.Month == 9 && date.Day >= 28) || (date.Month == 10 && date.Day <= 15))
            {
                res += "Сварожичи";
            }
            else if ((date.Month == 10 && date.Day >= 16) || (date.Month == 11 && date.Day <= 8))
            {
                res += "Морена";
            }
            else if (date.Month == 11 && date.Day >= 9 && date.Day <= 28)
            {
                res += "Зима";
            }
            else if (date.Month == 11 && date.Day >= 29 || (date.Month == 12 && date.Day <= 23))
            {
                res += "Карачун";
            }
            else
            {
                MessageBox.Show("Что-то пошло не так");
            }
            return res;
        }
    }
}