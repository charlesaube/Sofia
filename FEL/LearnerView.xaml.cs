using Sofia.DAL.Repository;
using Sofia.BLL.Service.util;
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
using Sofia.BLL.Service;
using Sofia.BLL.Service.bob;
using Sofia.BLL.Model;

namespace FEL
{
    /// <summary>
    /// Logique d'interaction pour LearnerView.xaml
    /// </summary>
    public partial class LearnerView : Page
    {   
        private Session session;
        private string requestScope;
        public OfferService offerService;
        IDAO dataService;
        private IList<TimeSlot> createdTimeSlots;
        public LearnerView()
        {
            InitializeComponent();
            session = new Session("Learner");
            dataService = new DatabaseDAO();
            offerService = new OfferService(dataService);
            requestScope = "Practice";
            createdTimeSlots = new List<TimeSlot>();
            DatePicker.SelectedDate = DateTime.Now;
            DateTextBox.Text = DatePicker.SelectedDate.Value.Year + "-" + DatePicker.SelectedDate.Value.Month + "-" + DatePicker.SelectedDate.Value.Day;
        }
            



        private void TopicList_Loaded(object sender, RoutedEventArgs e)
        {
            TopicList.ItemsSource = XPath_DAO.Instance.findAllTopic();
            TopicList.SelectedIndex =0;
        }

        private void ChangeConceptList(object sender, SelectionChangedEventArgs e)
        {
            ConceptList.ItemsSource = XPath_DAO.Instance.findAllConceptFromTopic(Convert.ToString(TopicList.SelectedItem));
            ConceptList.SelectedIndex = 0;
        }

        private void LoadUser(object sender, RoutedEventArgs e)
        {
            connectedUser.Content = session.currentUser.FirstName+" "+session.currentUser.LastName;
        }

        private void TimeSlotButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveTimeSlotButton.IsEnabled = true;
            SubmitRequest.IsEnabled = true;
            TimeDataGrid timeSlots = new TimeDataGrid(FromTextBox.Text, ToTextBox.Text, DatePicker.SelectedDate.Value);
            TimeSlotDataGrid.Items.Add(timeSlots);
            TimeSlot timeSlot = new TimeSlot(Preferences.__TIMESLOT_AUTO_ID++, DatePicker.SelectedDate.Value, FromTextBox.Text, ToTextBox.Text);
            createdTimeSlots.Add(timeSlot);

        }
        private void RemoveTimeSlotButton_Click(object sender, RoutedEventArgs e)
        {

            TimeSlotDataGrid.Items.RemoveAt(TimeSlotDataGrid.Items.Count - 1);
            createdTimeSlots.Remove(createdTimeSlots.Last());
            if (TimeSlotDataGrid.Items.Count == 0)
            {
                RemoveTimeSlotButton.IsEnabled = false;
                SubmitRequest.IsEnabled = false;
            }
                
        }

        private void SubmitRequest_Click(object sender, RoutedEventArgs e)
        {
            int filter;
            if (FilterChoice.SelectedItem.ToString() == "Likes")
                filter = 3;
            else
                if (FilterChoice.SelectedItem.ToString() == "Rate")
                filter = 2;
            else
                filter = 1;


            offerService.postTutoringRequest(Preferences.__REQUEST_AUTO_ID++, "Collège de Valleyfield", Convert.ToString(BackgroundComboBox.SelectedItem), requestScope,int.Parse(DurationComboBox.Text), XPath_DAO.Instance.findConceptByName(ConceptList.Text).ConceptId, session.currentUser.MemberId, filter);
            foreach (TimeSlot timeSlot in createdTimeSlots)
            {
                offerService.postLearnerTimeSlot(timeSlot, Preferences.__REQUEST_AUTO_ID - 1);
            }
            TimeSlotDataGrid.Items.Clear();
            DateTextBox.Text = null;
            FromTextBox.Text = null;
            ToTextBox.Text = null;
            createdTimeSlots.Clear();
            TimeSlotButton.IsEnabled = false;
            SubmitRequest.IsEnabled = false;
            RemoveTimeSlotButton.IsEnabled = false;
            SubmitPopUp.IsOpen = true;
        }

        private void ChangeSelectDate(object sender, SelectionChangedEventArgs e)
        {
            DateTextBox.Text = Convert.ToString(DatePicker.SelectedDate.Value.Year)+"-"+Convert.ToString(DatePicker.SelectedDate.Value.Month) + "-" + Convert.ToString(DatePicker.SelectedDate.Value.Day);
        }

        public class TimeDataGrid
        {
            public string from { get; set; }
            public string to { get; set; }
            public DateTime date { get; set; }
            public TimeDataGrid(string from,string to,DateTime date)
            {
                this.from = from;
                this.to = to;
                this.date = date;
            }
        }

        private void Practice_Clicked(object sender, RoutedEventArgs e)
        {
            requestScope = PraticeRadioButton.Content.ToString();
        }

        private void Theory_Clicked(object sender, RoutedEventArgs e)
        {
            requestScope = TheoryRadioButton.Content.ToString();
        }

        private void PracticeandTheory_Clicked(object sender, RoutedEventArgs e)
        {
            requestScope = PraticeRadioButton.Content.ToString();
        }
        private void PopUp_Click(object sender, RoutedEventArgs e)
        {
            TimeSlotButton.IsEnabled = true;
            SubmitRequest.IsEnabled = true;
            SubmitPopUp.IsOpen = false;
        }
    }
}
