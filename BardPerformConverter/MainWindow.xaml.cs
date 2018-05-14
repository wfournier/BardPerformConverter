using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace BardPerformConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string[]> keys = new Dictionary<string, string[]>();
        string[] notes;
        List<string[]> hotkeys = new List<string[]>();
        List<int> delays = new List<int>();
        int def_delay = 0;
        Stopwatch sw = new Stopwatch();
        int current_note_count = 0;
        bool done = false;
        public MainWindow()
        {
            InitializeComponent();
            tab_timing.IsEnabled = false;
            tab_output.IsEnabled = false;

            keys["C"] = new string[] { "Q" };
            keys["C#"] = new string[] { "2" };
            keys["D"] = new string[] { "W" };
            keys["Eb"] = new string[] { "3" };
            keys["E"] = new string[] { "E" };
            keys["F"] = new string[] { "R" };
            keys["F#"] = new string[] { "5" };
            keys["G"] = new string[] { "T" };
            keys["G#"] = new string[] { "6" };
            keys["A"] = new string[] { "Y" };
            keys["Bb"] = new string[] { "7" };
            keys["B"] = new string[] { "U" };
            keys["C-1"] = new string[] { "LCTRL", "Q" };
            keys["C#-1"] = new string[] { "LCTRL", "2" };
            keys["D-1"] = new string[] { "LCTRL", "W" };
            keys["Eb-1"] = new string[] { "LCTRL", "3" };
            keys["E-1"] = new string[] { "LCTRL", "E" };
            keys["F-1"] = new string[] { "LCTRL", "R" };
            keys["F#-1"] = new string[] { "LCTRL", "5" };
            keys["G-1"] = new string[] { "LCTRL", "T" };
            keys["G#-1"] = new string[] { "LCTRL", "6" };
            keys["A-1"] = new string[] { "LCTRL", "Y" };
            keys["Bb-1"] = new string[] { "LCTRL", "7" };
            keys["B-1"] = new string[] { "LCTRL", "U" };
            keys["C+1"] = new string[] { "I" };
            keys["C#+1"] = new string[] { "LSHIFT", "2" };
            keys["D+1"] = new string[] { "LSHIFT", "W" };
            keys["Eb+1"] = new string[] { "LSHIFT", "3" };
            keys["E+1"] = new string[] { "LSHIFT", "E" };
            keys["F+1"] = new string[] { "LSHIFT", "R" };
            keys["F#+1"] = new string[] { "LSHIFT", "5" };
            keys["G+1"] = new string[] { "LSHIFT", "T" };
            keys["G#+1"] = new string[] { "LSHIFT", "6" };
            keys["A+1"] = new string[] { "LSHIFT", "Y" };
            keys["Bb+1"] = new string[] { "LSHIFT", "7" };
            keys["B+1"] = new string[] { "LSHIFT", "U" };
            keys["C+2"] = new string[] { "LSHIFT", "I" };
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            text_input.Clear();
        }
        private void switch_Notes(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            TabControl.SelectedIndex = 0;
        }
        private void switch_Timing(object sender, RoutedEventArgs e)
        {
            string input = text_input.Text.ToString().Replace(",", "");
            input = input.Replace("♭", "b");
            input = input.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);

            notes = input.Split(new[] { " ", Environment.NewLine }, StringSplitOptions.None);

            foreach (string i in notes)
            {
                hotkeys.Add(keys[i]);
            }

            if (default_delay.Text == "")
            {
                try
                {
                    text_error.Text = "";
                    text_delay.Text = "";
                    tab_timing.IsEnabled = true;
                    TabControl.SelectedIndex = 1;

                    sw.Reset();
                    current_note_count = 0;
                    text_current_note.Text = notes[current_note_count];
                    delays.Clear();
                }
                catch (Exception ex)
                {
                    text_error.Text = "Invalid Notes";
                }
            }
            else
            {
                try
                {
                    def_delay = int.Parse(default_delay.Text.ToString());
                    switch_Output(sender, e);
                }
                catch (Exception ex)
                {
                    text_error.Text = "Invalid Default Delay";
                }
            }
        }
        private void switch_Output(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            tab_output.IsEnabled = true;
            TabControl.SelectedIndex = 2;

            string output = "";

            int count = 0;
            foreach (string[] i in hotkeys)
            {
                if (i.Length < 2)
                {
                    output += "<key direction=\"down\" value=\"" + i[0] + "\"/>";
                    output += Environment.NewLine;
                    output += "<key direction=\"up\" value=\"" + i[0] + "\"/>";
                    output += Environment.NewLine;

                    if (def_delay > 0)
                    {
                        output += "<delay milliseconds=\"" + def_delay + "\"/>";
                        output += Environment.NewLine;
                    }
                    else
                    {
                        if (count < delays.Count)
                        {
                            output += "<delay milliseconds=\"" + delays[count] + "\"/>";
                            output += Environment.NewLine;
                        }
                    }
                }
                else
                {
                    output += "<key direction=\"down\" value=\"" + i[0] + "\"/>";
                    output += Environment.NewLine;
                    output += "<key direction=\"down\" value=\"" + i[1] + "\"/>";
                    output += Environment.NewLine;
                    output += "<key direction=\"up\" value=\"" + i[1] + "\"/>";
                    output += Environment.NewLine;

                    if (def_delay > 0)
                    {
                        output += "<delay milliseconds=\"" + def_delay + "\"/>";
                        output += Environment.NewLine;
                    }
                    else
                    {
                        if (count < delays.Count)
                        {
                            output += "<delay milliseconds=\"" + delays[count] + "\"/>";
                            output += Environment.NewLine;
                        }
                    }

                    output += "<key direction=\"up\" value=\"" + i[0] + "\"/>";
                    output += Environment.NewLine;
                }
                count++;
            }

            //string list_notes = "";
            //foreach (string[] i in hotkeys)
            //{
            //    foreach (string j in i)
            //    {
            //        list_notes += j +"\n";
            //    }
            //}
            //MessageBox.Show(list_notes);

            //string list_delays = "";
            //foreach (int i in delays)
            //{
            //    list_delays += i +"\n";
            //}
            //MessageBox.Show(list_delays);

            text_output.Text = output;
        }

        private void key_press(object send, RoutedEventArgs e)
        {
            current_note_count++;

            if (current_note_count < notes.Length)
            {
                text_current_note.Text = notes[current_note_count];
            }

            sw.Stop();

            if (current_note_count != 1)
            {
                delays.Add((int)sw.ElapsedMilliseconds);
                text_delay.Text = (int)sw.ElapsedMilliseconds + " milliseconds";
            }

            sw.Reset();
            sw.Start();

            if (current_note_count == notes.Length)
            {
                switch_Output(send, e);
            }
        }

        private void grid_timing_Loaded(object send, RoutedEventArgs e)
        {
            grid_timing.Focus();
        }

        private void reset(object sender, RoutedEventArgs e)
        {
            notes = null;
            hotkeys.Clear();
            delays.Clear();

            text_input.Clear();
            text_output.Clear();
            default_delay.Clear();

            def_delay = 0;

            switch_Notes(sender, e);
        }
    }
}
