using Sofia.BLL.Model;
using Sofia.BLL.Service;
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
    /// Logique d'interaction pour MatchView.xaml
    /// </summary>
    public partial class MatchView : Page
    {
         OfferService offerService;
        MatchService matchService;
        IDAO dataService;
        IList<Match> allMatch;
        public MatchView()
        {
            InitializeComponent();
            dataService = new DatabaseDAO();
            offerService = new OfferService(dataService);
            matchService = new MatchService(dataService);
            allMatch = new List<Match>();
        }


        private void MatchButton_Click(object sender, RoutedEventArgs e)
        {
            TutoringOffer bestOffer;
            Member learner;
            Member tutor;
            TimeSlot time;
            foreach (Request request in dataService.findAllRequest())
            {
                bestOffer = matchService.findBestMatch(request);
                learner = dataService.findMemberByRequestId(request.RequestId);
                tutor = dataService.findMemberByTutoringOfferId(bestOffer.TutoringOfferId);
                time = dataService.findTimeSlotByTutoringOfferId(bestOffer.TutoringOfferId);
                offerService.postMeeting(request.RequestId, bestOffer.Duration, tutor.MemberId);
                MatchDataGrid.Items.Add(new Match(request.RequestId, tutor.FirstName + " " + tutor.LastName, learner.FirstName + " " + learner.LastName, request.Concept.Name,$"{time.Date.Year}-{time.Date.Month}-{time.Date.Day}",request.ExpectedDuration));
                
            }
            MatchButton.IsEnabled = false;

        }


        public class Match
        {
            public int RequestId { get; set; }
            public string TutorName { get; set; }
            public string LearnerName { get; set; }
            public string Concept { get; set; }

            public string TimeSlot { get; set; }
            public int ExpectedDuration { get; set; }

            public Match(int requestId, string tutorName, string learnerName, string concept, string timeSlot, int expectedDuration)
            {
                this.RequestId = requestId;
                this.TutorName = tutorName;
                this.LearnerName = learnerName;
                this.Concept = concept;
                this.TimeSlot = timeSlot;
                this.ExpectedDuration = expectedDuration;

            }

            public override string ToString()
            {
                return $" Id: {RequestId} Tutor: {TutorName} Learner: {LearnerName} Concept: {Concept}";
            }



        }
    }
}