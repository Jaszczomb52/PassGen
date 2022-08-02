namespace PassGen
{
    public partial class Form1 : Form
    {
        List<string> adj;
        List<string> noun;
        List<string> verb;
        string word1;
        string word2;
        string word3;
        Dictionary<string, string> chars = new Dictionary<string, string>();
        Dictionary<string, string> numbers = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
            var file = File.ReadAllLines("..\\..\\..\\eng-adj.txt");
            adj = new List<string>(file);
            file = File.ReadAllLines("..\\..\\..\\eng-noun.txt");
            noun = new List<string>(file);
            file = File.ReadAllLines("..\\..\\..\\eng-verb.txt");
            verb = new List<string>(file);

            comboBox2.DataSource = noun;
            comboBox3.DataSource = verb;
            comboBox1.DataSource = adj;

            try
            {
                chars = Methods<string, string>.ListsIntoDict(
                    new List<string>() { "a", "i", "l", "e", "s" },
                    new List<string>() { "@", "!", "!", "€", "$" },
                    chars
                    );
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Error during converting lists into dictionary");
            }

            try
            {
                numbers = Methods<string, string>.ListsIntoDict(
                    new List<string>() { "i", "l", "e", "s", "a", "b", "g", "o" },
                    new List<string>() { "1", "7", "3", "5", "4", "6", "9", "0" },
                    numbers
                    );
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Error during converting lists into dictionary");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            word1 = comboBox2.Text;
            word2 = comboBox3.Text;
            word3 = comboBox1.Text;
            try
            {
                ModifyText(new string[] { word1, word2, word3 },
                    Convert.ToInt32(numericUpDown1.Value),
                    Convert.ToInt32(numericUpDown2.Value),
                    Convert.ToInt32(numericUpDown3.Value));
            }
            catch
            {
                MessageBox.Show("Error. Be sure to fill every word field.");
            }
        }

        void ModifyText(string[] words, int chars, int numbers, int upper)
        {
            char[] word = (words[0] + words[1] + words[2]).ToCharArray();
            if (!checkBox1.Checked)
            {
                if (word.Length / 6 > 2)
                {
                    chars = word.Length / 6;
                    numbers = word.Length / 6;
                    upper = word.Length / 6;
                }
                else
                {
                    chars = 2;
                    numbers = 2;
                    upper = 2;
                }
            }
            Random r = new Random();
            int i = 0;
            int iter = 0;
            while (chars > 0)
            {
                if (iter > 5)
                    break;
                if (i > word.Length - 1)
                {
                    i = 0;
                    iter++;
                }

                if (r.Next(0, 2) == 0)
                {
                    i++;
                    continue;
                }

                if (this.chars.ContainsKey(word[i].ToString()))
                {
                    word[i] = this.chars[word[i].ToString()].ToCharArray()[0];
                    i++;
                    chars--;
                }
            }
            i = 0;
            iter = 0;
            while (numbers > 0)
            {
                if (iter > 5)
                    break;
                if (i > word.Length - 1)
                {
                    i = 0;
                    iter++;
                }

                if (r.Next(0, 2) == 0)
                {
                    i++;
                    continue;
                }

                if (this.numbers.ContainsKey(word[i].ToString()))
                {
                    word[i] = this.numbers[word[i].ToString()].ToCharArray()[0];
                    i++;
                    numbers--;
                }
            }

            while (upper > 0)
            {
                if (iter > 5)
                    break;
                if (i > word.Length - 1)
                {
                    i = 0;
                    iter++;
                }

                if (r.Next(0, 2) == 0)
                {
                    i++;
                    continue;
                }

                if (Char.IsLetter(word[i]))
                {
                    word[i] = Char.ToUpper(word[i]);
                    i++;
                    upper--;
                }
            }

            textBox1.Text = new string(word);
        }
        private void Update(object sender, EventArgs e)
        {
            cbUpdate(new RadioButton[] { radioButton1, radioButton2, radioButton3 }, comboBox2);
        }
        private void Update2(object sender, EventArgs e)
        {
            cbUpdate(new RadioButton[] { radioButton6, radioButton7, radioButton8 }, comboBox3);
        }

        private void Update3(object sender, EventArgs e)
        {
            cbUpdate(new RadioButton[] { radioButton9, radioButton10, radioButton11 }, comboBox1);
        }



        void cbUpdate(RadioButton[] rad, ComboBox cb)
        {
            if (rad[0].Checked)
                cb.DataSource = adj;
            else if (rad[1].Checked)
                cb.DataSource = noun;
            else
                cb.DataSource = verb;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                tableLayoutPanel2.Visible = true;
            else
                tableLayoutPanel2.Visible = false;
        }
    }

    public static class Methods<T,t>
    {
        public static Dictionary<T,t> ListsIntoDict(List<T> keys, List<t> values, Dictionary<T,t> dict)
        {
            if (keys is null)
                throw new ArgumentNullException(nameof(keys));
            if (values is null)
                throw new ArgumentNullException(nameof(values));
            if (dict is null)
                throw new ArgumentNullException(nameof(dict));
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] is null)
                    continue;
                if (values[i] is null)
                    continue;
                dict.Add(keys[i], values[i]);
            }
            return dict;
        }
    }
}