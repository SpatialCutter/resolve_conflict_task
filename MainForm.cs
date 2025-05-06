using BookShopAnalitics.Entities;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.AvalonDock.Layout;

namespace BookShopAnalitics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Graph graph;
        ObservableCollection<LegendItem> lines;
        DbBookShopContext db;

        int statwidth;
        bool showstat;

        public MainWindow()
        {
            InitializeComponent();
            lines = new ObservableCollection<LegendItem>();
            graph = new Graph();
            db = new DbBookShopContext();
            DataContext = graph;
            lbLegend.ItemsSource = lines;
            statwidth = (int)colStat.Width.Value;
            showstat = true;
        }

        void BLegendAddClick(object sender, RoutedEventArgs e)
        {
            BAddClick(-1);         
        }

        void BAddClick(int sender)
        {
            var editline = new EditLine();
            if (editline.ShowDialog().Value)
            {
                var line = editline.GetLine();
                line.num = sender+1;
                LegendItem li = new LegendItem(line);
                li.OnLineAdded += BAddClick;
                li.OnLineRemoved += BRemoveClick;
                li.OnLineEdited += RefreshGraph;
                li.Refresh();
                li.line.Refresh();
                li.analysis = new Analysis(line);
                li.analysis.Refresh();
                lines.Insert(sender+1, li);

                graph.Draw(li.analysis, sender+1);
                LineNumsRefresh(sender + 1, true);
                lbLegendAdd.Visibility = Visibility.Hidden;
                lTrends.Content = li.analysis.GetTendency(); 
                lTrends.Content += li.analysis.GetTrends();
                lSeason.Content = li.analysis.GetSeason();
                lAnomaly.Content = li.analysis.GetAnomaly();
            }            
        }

        void BRemoveClick(int sender)
        {
            graph.Undraw(sender);
            LineNumsRefresh(sender, false);
            lines.RemoveAt(sender);            
            if (lines.Count == 0) lbLegendAdd.Visibility = Visibility.Visible;
        }

        void LineNumsRefresh(int curnum, bool increment)
        {
            if (curnum != lines.Count)
            {
                for (int i = curnum + 1; i < lines.Count; i++)
                {
                    lines[i].line.num += increment ? 1 : -1;
                    lines[i].Refresh();
                }
            }
        } 

        void RefreshGraph(LegendItem sender)
        {
            graph.Redraw(sender.analysis, sender.line.num);
            lAnomaly.Content = sender.analysis.GetAnomaly();
            lSeason.Content = sender.analysis.GetSeason();
            lTrends.Content = sender.analysis.GetTendency();
            lTrends.Content += sender.analysis.GetTrends();
        }

        private void HideWorkspace()
        {
            DeactiveTab(tabDistrib);
            DeactiveTab(tabGraph);
            DeactiveTab(tabTable);
            chart.Visibility = Visibility.Collapsed;
            scrollchart.Visibility = Visibility.Collapsed;
            distribchart.Visibility = Visibility.Collapsed;
        }

        private void DeactiveTab(Button btn)
        {
            btn.Background = Brushes.LightGray;
            btn.Foreground = Brushes.Gray;
            btn.BorderBrush = Brushes.Gray;
        }

        private void ActiveTab(Button btn)
        {
            btn.Background = Brushes.White;
            btn.Foreground = SystemParameters.WindowGlassBrush;
            btn.BorderBrush = SystemParameters.WindowGlassBrush;
        }

        private void tabStat_Click(object sender, RoutedEventArgs e) { }
    }
}
