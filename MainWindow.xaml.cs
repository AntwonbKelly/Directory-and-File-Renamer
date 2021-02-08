using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.IO;

namespace Directory_and_File_Renamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> DirectoryList = new ObservableCollection<string>();
        Char[] ListOfCharacters;
        String CharactersToBeDeleted; //Stores characters to be dleeted
        String Location; //Stores location of main directory
        string[] Directories; //Stories Directorie names
        List<String> foldernames = new List<string>();
        int i = 0; // Control Variable
        int l = 0; // Control Variable
        String WordToBeDeleted; // Stores word to be deleted
        public MainWindow()
        {
            InitializeComponent();
        }

        #region SelectFolder Button
        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                

                System.Windows.MessageBox.Show(FBD.SelectedPath);


                Location = FBD.SelectedPath;

                Directories = System.IO.Directory.GetDirectories
                    (Location, "*", System.IO.SearchOption.AllDirectories); //Gets subdirectory names in directory
                int count = Directories.Count();
                if (count < 1)
                {
                    System.Windows.MessageBox.Show("This directory has no directories to name!");
                    return;
                }
                
            }
            
            DataContext = Directories; //Gives folders names to the listbox through the data context.
            
        }
        #endregion

        #region Enter Button
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {

            if(CharList.Text == null)
            {
                System.Windows.MessageBox.Show("You need to enter characters to be removed!");
                return;
            }
            CharactersToBeDeleted = CharList.Text; //Sets words equal to the characters form the extbox

            ListOfCharacters = CharactersToBeDeleted.ToCharArray(); //Extracts the characters from the string in 
            //words to be deleted and stores them in the global array

            CharList.Clear();

            ListBoxChar.DataContext = ListOfCharacters;
            
        }
        #endregion


        #region RemoveButton
        private void RemoveCharactersButton_Click(object sender, RoutedEventArgs e)
        {
            if(Location ==null)
            {
                System.Windows.MessageBox.Show("You need to selecte a Directory first!");
                return;
            }
            
            foreach (String S in Directories)
            {
                
                foldernames.Add(System.IO.Path.GetFileName(S));
            }

            foreach(String names in Directories)
            {
                string sentence = names;
                string ChangedFileName =  System.IO.Path.GetFileName(sentence); //Extracts file name
                string CombineFilePath = System.IO.Path.GetDirectoryName(names); //Exctracts path
                string[] words = ChangedFileName.Split(ListOfCharacters); //Edits file name based on characters enteered

                words[0] = string.Join("", words); //Combines all remaining words of Array into a single element.
                string finished = System.IO.Path.Combine(CombineFilePath, words[0]); //Combines the path and the NEW file name

                

                if (names == finished)
                {
                    continue;

                }

                if(words[0] == "")
                {
                    i++;

                    

                    finished = System.IO.Path.Combine(CombineFilePath, "Default" + i);
                    //If a file doens't have a name, it creates a default name and increments i
                    // to avoid getting a same name crash 

                    if (ChangedFileName == "Default1" || ChangedFileName == "Default2")
                    {
                        
                        l = 10;
                        l++;
                        finished = System.IO.Path.Combine(CombineFilePath, "Default" + l);
                    }

                }
                Directory.Move(names, finished);
            }

            
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }
        #endregion

        #region WordTextBox
        private void WordTextBox_Click(object sender, RoutedEventArgs e)
        {
            WordToBeDeleted = WordList.Text;

            WordListBox.DataContext = WordToBeDeleted;
            WordList.Clear();

        }

        #endregion

        #region Remove Word Button
        private void RemoveWordButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (String names in Directories)
            {
                string sentence = names;
                string ChangedFileName = System.IO.Path.GetFileName(sentence); //Extracts file name
                string CombineFilePath = System.IO.Path.GetDirectoryName(names); //Exctracts path
                string words = ChangedFileName.Replace(WordToBeDeleted, ""); //Edits file name based on characters enteered

                
                string finished = System.IO.Path.Combine(CombineFilePath, words); //Combines the path and the NEW file name



                if (names == finished)
                {
                    continue;

                }

                if (words == "")
                {
                    i++;

                    finished = System.IO.Path.Combine(CombineFilePath, "Default" + i);
                    //If a file doens't have a name, it creates a default name and increments i
                    // to avoid getting a same name crash 

                    if (ChangedFileName == "Default1" || ChangedFileName == "Default2")
                    {

                        int l = 10;
                        l++;
                        finished = System.IO.Path.Combine(CombineFilePath, "Default" + l);
                    }

                }
                Directory.Move(names, finished);
            }


            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }

    }
    #endregion
}
