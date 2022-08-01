namespace PassGen
{
    public partial class Form1 : Form
    {
        List<string> adj;
        List<string> noun;
        List<string> verb;
        public Form1()
        {
            InitializeComponent();
            var file = File.ReadAllLines("C:\\Users\\Kacper\\source\\repos\\PassGen\\PassGen\\eng-adj.txt");
            adj = new List<string>(file);
            file = File.ReadAllLines("C:\\Users\\Kacper\\source\\repos\\PassGen\\PassGen\\eng-noun.txt");
            noun = new List<string>(file);
            file = File.ReadAllLines("C:\\Users\\Kacper\\source\\repos\\PassGen\\PassGen\\eng-verb.txt");
            verb = new List<string>(file);

            comboBox1.DataSource = adj;
            comboBox2.DataSource = noun;
            comboBox3.DataSource = verb;
        }
    }
}