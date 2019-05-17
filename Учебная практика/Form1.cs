using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учебная_практика
{
    public partial class Main_Form : Form
    {
        public string[] Alive = null;
        public string[] NotAliveYet = null;
        public string[] NotDeadYet = null;
        public int job=1;
        public int Speed_;
        public Main_Form()
        {
            InitializeComponent();
        }
        private void Main_Form_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Выбирая ввод скорости или ввод поколения, вы выбираете режим работы программы \n Для возврата к изначальному состоянию программы нажмите *Очистить* \n Подробности в пункте справка, приятной игры!");
        }
        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var item in Controls) //обходим все элементы формы
            {
                if (item is Button) // проверяем, что это кнопка
                {
                    if (item is TableLayoutPanel)//Проверяем принадлежность таблице
                    {
                        ((Button)item).Click += A1_Click; //приводим к типу и устанавливаем обработчик события  
                    }
                }
            }
        }//Обработка оживления
        private void A1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.White)
            {
                ((Button)sender).BackColor = Color.Green;
            }
            else
            {
                ((Button)sender).BackColor = Color.White;
            }
        }//Оживляение клеток
        private void EnterButton_Click(object sender, EventArgs e)
        {
            CheckButtonsNow(sender, e);
            tableLayoutPanel1.Enabled = false;
            Step.Enabled = true;
            EnterButton.Enabled = false;
            MassiveAlive(sender, e);
            Print(sender, e);
            EnterSpeed.Enabled = false;
        }//Вводим первое поколение для пошагового выполнения, обходя кнопки
        private void CheckButtonsNow(object sender, EventArgs e)
        {
            int[,] MassiveCondition = new int[12, 12];
            int Sum = 0;
            int Condition = 0;
            foreach (var Buttons in tableLayoutPanel1.Controls)
            {
                if (Buttons is Button)
                {
                    if (((Button)Buttons).BackColor == Color.Green)
                    {
                        Condition = 1;
                        int i = this.tableLayoutPanel1.GetRow((Button)Buttons);
                        int j = this.tableLayoutPanel1.GetColumn((Button)Buttons);
                        MassiveCondition[i, j] = Condition;
                    }
                    else
                    {
                        Condition = 0;
                    }
                }
            }//Записываем живые клетки
            foreach (var Cell in tableLayoutPanel1.Controls)
            {
                if (Cell is Button)
                {
                    int i = this.tableLayoutPanel1.GetRow((Button)Cell);
                    int j = this.tableLayoutPanel1.GetColumn((Button)Cell);
                    if (MassiveCondition[i + 1,j-1]==1 )
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i + 1, j] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i + 1,j+1]==1 )
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i , j + 1] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i , j - 1] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i - 1, j - 1] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i - 1, j] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i - 1, j + 1] == 1)
                    {
                        Sum++;
                    }
                    //Оббегаем соседние кнопки, считаем живые
                    if (Cell is Button)
                    {
                        if (Sum == 3 && ((Button)Cell).BackColor == Color.White)
                            ((Button)Cell).BackColor = Color.Yellow;//Оживающая клетка окрашивается в желтый
                        if ((Sum < 2 || Sum > 3) && ((Button)Cell).BackColor == Color.Green)
                            ((Button)Cell).BackColor = Color.Red;//Умирающая клетка становится красной
                        if ((Sum >= 2 || Sum <= 3) && ((Button)Cell).BackColor == Color.Green)
                            ((Button)Cell).BackColor = Color.Green;//Живая клетка живет при наличии 2 или 3 соседей
                        if ((Sum < 2 || Sum > 3) && ((Button)Cell).BackColor == Color.White)
                            ((Button)Cell).BackColor = Color.White;//Мертвая клетка не оживает при наличии менее 2 и более 3 живых соседей
                        Sum = 0;
                    }
                }
            }
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    MassiveCondition[i, j] = 0;
                }
            }//Выполнение по шагам //Зануляем массив состояний в конце
        }//Проверка состояния матрицы, поиск живых, предсказание на будущее
        private void CheckButtonsNext(object sender, EventArgs e)
        {
            foreach (var Buttons in tableLayoutPanel1.Controls)
            {
                if (Buttons is Button)
                {
                    if (((Button)Buttons).BackColor == Color.Yellow)
                        ((Button)Buttons).BackColor = Color.Green;//Оживающая желтая клетка оживает
                    if (((Button)Buttons).BackColor == Color.Red)
                        ((Button)Buttons).BackColor = Color.White;//Умирающая красная клетка умирает
                }
            }
        }//Проверка после шага
        private void MassiveAlive(object sender, EventArgs e)
        {
             int alive = 0;
             int NotAlive = 0;
             int NotDead = 0;
            foreach (var Button in tableLayoutPanel1.Controls)
            {
                if (Button is Button)
                {
                    if (((Button)Button).BackColor == Color.Green)
                        alive++;
                    if (((Button)Button).BackColor == Color.Red)
                        NotDead++;
                    if (((Button)Button).BackColor == Color.Yellow)
                        NotAlive++;
                }
            }//Считаем клетки
            string[] Alive_ = new string[alive];
            string[] NotAliveYet_ = new string[NotAlive];
            string[] NotDeadYet_ = new string[NotDead];
            int a = 0;
            int b = 0;
            int c = 0;
            foreach (var Butto in tableLayoutPanel1.Controls)
            {
                if (Butto is Button)
                {
                    if (((Button)Butto).BackColor == Color.Green)
                    {
                        Alive_[a] = ((Button)Butto).Name;
                        a++;
                    }

                    if (((Button)Butto).BackColor == Color.Red)
                        {
                            NotDeadYet_[b] = ((Button)Butto).Name;
                            b++;
                        }
                    if (((Button)Butto).BackColor == Color.Yellow)
                        {
                            NotAliveYet_[c] = ((Button)Butto).Name;
                            c++;
                        }
                }
            }//Заносим клетки в текстбоксы ванги
            Alive = Alive_; NotAliveYet = NotAliveYet_; NotDeadYet = NotDeadYet_;
            Alive_ = null; NotAliveYet_ = null; NotDeadYet_ = null;
             alive = 0;
             NotAlive = 0;
             NotDead = 0;
        }//Массив кнопок для вывода в бокс при пошаговом выполнении
        private void Print(object sender, EventArgs e)
        {
            MassiveAlive(sender, e);
            for(int i = 0; i < Alive.Length; i++)
            {
                Present.Text += Alive[i]+' ';
            }
            for (int i = 0; i < NotDeadYet.Length; i++)
            {
                Present.Text += NotDeadYet[i]+' ';
            }//Список "Живых клеток на текущей итерации
            if (NotDeadYet.Length != 0)
            {
                Future.Text += " Умрут на следующий ход (Красные):";
                Present.Text += " Красные клетки умирают";
                for (int i = 0; i < NotDeadYet.Length; i++)
                {
                    Future.Text += NotDeadYet[i] + ' ';
                }
            }//Список умирающих клеток
            if (NotAliveYet.Length != 0)
            {
                Future.Text += " Оживут на следующий ход(Желтые):";
                Present.Text += " Желтые оживают";
                for (int i = 0; i < NotAliveYet.Length; i++)
                {
                    Future.Text += NotAliveYet[i] + ' ';
                }
            }//Список оживающих клеток
            if(Alive.Length==0&&NotAliveYet.Length==0)
            {
                Future.Text += " На следующий ход не останется живых клеток";
            }//Проверка 
            if (Alive.Length != 0 && NotAliveYet.Length == 0 && NotDeadYet.Length==0)
            {
                Future.Text += " На следующий ход состояние матрицы не изменится";
            }//Проверка наличия устойчивых структур
            if (Alive.Length == 0 && NotDeadYet.Length == 0)
            {
                Present.Text += " На поле нет живых клеток";
                tableLayoutPanel1.Enabled = true;
                Step.Enabled = false;
                EnterButton.Enabled = true;
            }//Проверка наличия живых клеток
        }//Печать в текст боксы ванги
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var Buttons in tableLayoutPanel1.Controls)
            {
                if (Buttons is Button)
                {
                    ((Button)Buttons).BackColor = Color.White;
                }
            }
            tableLayoutPanel1.Enabled = true;
            EnterButton.Enabled = true;
            Step.Enabled = false;
            Present.Text = "Нынешнее поколение: Живые клетки (Зеленые и красные): ";
            Future.Text = "Будуще поколение:";
            Life.Enabled = false;
            EnterSpeed.Enabled = true;
            job = 1;
        }//Кнопка очищения матрицы
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Работу выполнил Петухов Александр Сергеевич студент группы Би-17-2 \n Для начала работы задайте состояние клеток и введите поколение, после ввода поколения матрица станет неактивной \n Для перехода на след. итерацию нажмите кнопку Следующий шаг \n Для изучения правил и примеров нажмите соответствующие кнопки \n Для возврата к начальному состоянию нажмите кнопку очистить \n Для циклического выполнения введите скорость и нажмите старт, \n Для завершения кликните на старт второй раз");
        }//Кнопка справки
        private void Step_Click(object sender, EventArgs e)
        {
            Present.Text = "Нынешнее поколение: Живые клетки (Зеленые и красные): ";
            Future.Text = "Будуще поколение:";
            CheckButtonsNext(sender, e);
            CheckButtonsNow(sender, e);
            Print(sender, e);
        }//Следующий шаг
        private void EnterSpeed_Click(object sender, EventArgs e)
        {
            EnterButton.Enabled = false;
            Life.Enabled = true;
            Speed_ = Convert.ToInt32(Speed.Value) * 10;
        }//Ввод скорости для циклического выполнения
        private void NotStep()
        {
            int[,] MassiveCondition = new int[12, 12];
            int Sum = 0;
            int Condition = 0;
            foreach (var Buttons in tableLayoutPanel1.Controls)
            {
                if (Buttons is Button)
                {
                    if (((Button)Buttons).BackColor == Color.Green)
                    {
                        Condition = 1;
                        int i = this.tableLayoutPanel1.GetRow((Button)Buttons);
                        int j = this.tableLayoutPanel1.GetColumn((Button)Buttons);
                        MassiveCondition[i, j] = Condition;
                    }
                    else
                    {
                        Condition = 0;
                    }
                }
            }//Записываем живые клетки
            foreach (var Cell in tableLayoutPanel1.Controls)
            {
                if (Cell is Button)
                {
                    int i = this.tableLayoutPanel1.GetRow((Button)Cell);
                    int j = this.tableLayoutPanel1.GetColumn((Button)Cell);
                    if (MassiveCondition[i + 1, j - 1] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i + 1, j] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i + 1, j + 1] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i, j + 1] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i, j - 1] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i - 1, j - 1] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i - 1, j] == 1)
                    {
                        Sum++;
                    }
                    if (MassiveCondition[i - 1, j + 1] == 1)
                    {
                        Sum++;
                    }
                    //Оббегаем соседние кнопки, считаем живые
                    if (Cell is Button)
                    {
                        if (Sum == 3 && ((Button)Cell).BackColor == Color.White)
                            ((Button)Cell).BackColor = Color.Green;//Оживающая клетка окрашивается в желтый
                        if ((Sum < 2 || Sum > 3) && ((Button)Cell).BackColor == Color.Green)
                            ((Button)Cell).BackColor = Color.White;//Умирающая клетка становится красной
                        if ((Sum >= 2 || Sum <= 3) && ((Button)Cell).BackColor == Color.Green)
                            ((Button)Cell).BackColor = Color.Green;//Живая клетка живет при наличии 2 или 3 соседей
                        if ((Sum < 2 || Sum > 3) && ((Button)Cell).BackColor == Color.White)
                            ((Button)Cell).BackColor = Color.White;//Мертвая клетка не оживает при наличии менее 2 и более 3 живых соседей
                        Sum = 0;
                    }
                }
            }
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    MassiveCondition[i, j] = 0;
                }
            }
        }
        private void примерыСтруктурАвтоматаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            fr2.Show();
        }//Вызов картинки с примерами
        private async void Life_Click(object sender, EventArgs e)
        {
            job += 1;
            do
            {
                await Task.Delay(Speed_);
                NotStep();
            } while (job % 2 == 0);
        }
    }
}
