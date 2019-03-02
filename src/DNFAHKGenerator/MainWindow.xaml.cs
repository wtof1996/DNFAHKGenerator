using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using DNFAHKGenerator.Model;
using DNFAHKGenerator.Parser;
using Microsoft.Win32;

namespace DNFAHKGenerator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Skill> SkillList { get; set; } = new ObservableCollection<Skill>();
        public MainWindow()
        {
            InitializeComponent();

            DataGridSkills.ItemsSource = SkillList;

        }

        private void BtnAddSkill_Click(object sender, RoutedEventArgs e)
        {
            Skill newSkill = new Skill()
            {
                Id = SkillList.Count + 1,
                SkillName = "",
                HotKeys = ""
            };

            SkillList.Add(newSkill);
        }

        private void DataGridSkills_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(e.EditAction != DataGridEditAction.Commit) return;
            if(e.Column != ColumnHotKeys) return;

            var newText = e.EditingElement as TextBox;

            Debug.Assert(newText != null, nameof(newText) + " != null");
            newText.Text = HotKeyConverter.ParseInput(newText.Text);

        }

        private void BtnSaveSkill_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog()
            {
                FileName = "skill",
                DefaultExt = "json",
                Filter = "技能文件 (.json)|*.json"
            };

            var result = saveDialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            try
            {
                FileParser.SaveSkillFile(saveDialog.FileName, SkillList);
                MessageBox.Show("保存成功");
            }
            catch (IOException ex)
            {
                MessageBox.Show("技能文件保存失败！" + ex.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("程序出现未知错误！");
            }

        }

        private void BtnLoadSkill_Click(object sender, RoutedEventArgs e)
        {
            var loadDialog = new OpenFileDialog()
            {
                Filter = "技能文件 (.json)|*.json"
            };

            var result = loadDialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            List<Skill> newSkillList;

            try
            {
                newSkillList = FileParser.LoadSkillFile(loadDialog.FileName);
            }
            catch (IOException ex)
            {
                MessageBox.Show("技能文件读取失败！" + ex.ToString());
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("程序出现未知错误！");
                return;
            }

            if (newSkillList.Count > 0)
            {
                SkillList.Clear();
                foreach (var skill in newSkillList)
                {
                    SkillList.Add(skill);
                }
            }

        }
    }
}
