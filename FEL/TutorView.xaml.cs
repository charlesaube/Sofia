using Sofia.BLL.Model;
using Sofia.BLL.Service;
using Sofia.BLL.Service.bob;
using Sofia.BLL.Service.util;
using Sofia.DAL.Repository;
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

namespace FEL
{
    /// <summary>
    /// Logique d'interaction pour TutorView.xaml
    /// </summary>
    public partial class TutorView : Page
    {
        private Session session;
        public OfferService offerService;
        IDAO dataService;
        private IList<TimeSlot> createdTimeSlots;
        public TutorView()
        {
            InitializeComponent();
            session = new Session("Tutor");
            dataService = new DatabaseDAO();
            offerService = new OfferService(dataService);
            createdTimeSlots = new List<TimeSlot>();
            DatePicker.SelectedDate = DateTime.Now;
            DateTextBox.Text = DatePicker.SelectedDate.Value.Year + "-" + DatePicker.SelectedDate.Value.Month + "-" + DatePicker.SelectedDate.Value.Day;
        }


        private void TopicList_Loaded(object sender, RoutedEventArgs e)
        {
            TopicList.ItemsSource = XPath_DAO.Instance.findAllTopic();
            TopicList.SelectedIndex = 0;
        }

        private void ChangeConceptList(object sender, SelectionChangedEventArgs e)
        {
            ConceptList.ItemsSource = XPath_DAO.Instance.findAllConceptFromTopic(Convert.ToString(TopicList.SelectedItem));
            ConceptList.SelectedIndex = 0;
        }

        private void LoadUser(object sender, RoutedEventArgs e)
        {
            connectedUser.Content = session.currentUser.FirstName + " " + session.currentUser.LastName;
        }
        private void TimeSlotButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveTimeSlotButton.IsEnabled = true;
            SubmitOffer.IsEnabled = true;
            TimeDataGrid timeSlots = new TimeDataGrid(FromTextBox.Text, ToTextBox.Text, DatePicker.SelectedDate.Value);
            TimeSlotDataGrid.Items.Add(timeSlots);
            TimeSlot timeSlot = new TimeSlot(Preferences.__TIMESLOT_AUTO_ID++, DatePicker.SelectedDate.Value, FromTextBox.Text, ToTextBox.Text);
            createdTimeSlots.Add(timeSlot);

        }
        private void RemoveTimeSlotButton_Click(object sender, RoutedEventArgs e)
        {

            TimeSlotDataGrid.Items.RemoveAt(TimeSlotDataGrid.Items.Count-1);
            createdTimeSlots.Remove(createdTimeSlots.Last());

            if (TimeSlotDataGrid.Items.Count == 0)
            {
                SubmitOffer.IsEnabled = false;
                RemoveTimeSlotButton.IsEnabled = false;
            }
                
        }
            private void ChangeSelectDate(object sender, SelectionChangedEventArgs e)
        {
            DateTextBox.Text = Convert.ToString(DatePicker.SelectedDate.Value.Year) + "-" + Convert.ToString(DatePicker.SelectedDate.Value.Month) + "-" + Convert.ToString(DatePicker.SelectedDate.Value.Day);
        }
        private void SubmitOffer_Click(object sender, RoutedEventArgs e)
        {
            offerService.postTutoringOffer(Preferences.__TUTORINGOFFER_AUTO_ID++, session.currentUser.MemberId, XPath_DAO.Instance.findConceptByName(ConceptList.Text).ConceptId, int.Parse(ExpertiseComboBox.Text), int.Parse(RateComboBox.Text),int.Parse(DurationComboBox.Text));
            foreach (TimeSlot timeSlot in createdTimeSlots)
            {
                offerService.postTutorTimeSlot(timeSlot, Preferences.__TUTORINGOFFER_AUTO_ID -1);
            }


            TimeSlotDataGrid.Items.Clear();
            DateTextBox.Text = null;
            FromTextBox.Text = null;
            ToTextBox.Text = null;
            createdTimeSlots.Clear();
            TimeSlotButton.IsEnabled = false;
            SubmitOffer.IsEnabled = false;
            RemoveTimeSlotButton.IsEnabled = false;
            SubmitPopUp.IsOpen = true;
        }

        public class TimeDataGrid
        {
            public string from { get; set; }
            public string to { get; set; }
            public DateTime date { get; set; }
            public TimeDataGrid(string from, string to, DateTime date)
            {
                this.from = from;
                this.to = to;
                this.date = date;
            }
        }

        private void PopUp_Click(object sender, RoutedEventArgs e)
        {
            TimeSlotButton.IsEnabled = true;
            SubmitOffer.IsEnabled = true;
            SubmitPopUp.IsOpen = false;
        }


    }
}
