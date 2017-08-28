using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_Reproductor
{
    public partial class Form1 : Form
    {
        Dictionary<string, Song> SongList;
        LinkedList<Song> PlayList;

        public Form1()
        {
            SongList = new Dictionary<string, Song>();
            PlayList = new LinkedList<Song>();
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string songName = "";
            Song songToAdd = new Song();
            string songPath = "";
            TimeSpan duration;

            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "Music(.mp3) | *.mp3";
         
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                songPath = openfiledialog.FileName;         
            }

            TagLib.File song = TagLib.File.Create(songPath, TagLib.ReadStyle.Average);
            duration = song.Properties.Duration;
            songName = song.Tag.Title;

            songToAdd.songName = songName;
            songToAdd.songDuration = duration;

            SongList.Add(songToAdd.songName, songToAdd);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string songToSearch = textBox2.Text;
            if (SongList.ContainsKey(songToSearch))
                listBox1.Items.Add(SongList[songToSearch].songName);
            else
                MessageBox.Show("Ésta canción no existe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Song song in SongList.Values)
            {
                listBox1.Items.Add(song.songName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //"NOMBRE: " + song.songName + "|" + string.Format(" DURACIÓN: {0} minutos y {1} segundos", song.songDuration.Minutes, song.songDuration.Seconds)
            Song songToPlaylist = new Song();
            songToPlaylist.songName = SongList[listBox1.SelectedItem.ToString()].songName;
            songToPlaylist.songDuration = SongList[listBox1.SelectedItem.ToString()].songDuration;

            PlayList.AddLast(songToPlaylist);

            listBox2.Items.Add("NOMBRE: " + songToPlaylist.songName + "|" + string.Format(" DURACIÓN: {0} minutos y {1} segundos", songToPlaylist.songDuration.Minutes, songToPlaylist.songDuration.Seconds));

        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            PlayList.OrderBy(song => song.songName);

            foreach(Song song in PlayList)
            {
                listBox2.Items.Add("NOMBRE: " + song.songName + "|" + string.Format(" DURACIÓN: {0} minutos y {1} segundos", song.songDuration.Minutes, song.songDuration.Seconds));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            PlayList.OrderByDescending(song=> song.songName);

            foreach (Song song in PlayList)
            {
                listBox2.Items.Add("NOMBRE: " + song.songName + "|" + string.Format(" DURACIÓN: {0} minutos y {1} segundos", song.songDuration.Minutes, song.songDuration.Seconds));
            }
        }
    }
}
