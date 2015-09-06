using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKS
{
    public class Programe
    {
        const int DECIMAL_DIGITS = 1;

        public const int MAX_START_AGE = 15;

        public const int MIN_START_AGE = 4;

        public const int SCOUT_BORDER_AGE = 11;

        public const int ROVER_BORDER_AGE = 16;

        const Boolean EXCLUDE_UNCOMPLETE_RECORDS = true;

        readonly DateTime FIRST_REGISTRATION_DATE = DateTime.Parse("2003-01-01");

        public ObservableCollection<Row> LoadedRows { get; private set; }

        public ObservableCollection<Record> Records { get; private set; }

        public List<Info> Informations { get; private set; }

        public List<Info> LearningSet { get; private set; }

        public List<Info> TestSet { get; private set; }

        public int LearningPercent { get; private set; }

        public float Step { get; private set; }

        public float StopCondition { get; private set; }

        private DataLoader loader;

        private DataTransformer transformer;

        public Graph Graph { get; private set; }

        public Programe()
        {
            loader = new DataLoader();
            transformer = new DataTransformer();
            Graph = new Graph();
        }

        public void LoadData(string FilePath)
        {
            loader.FilePath = FilePath; 
            LoadedRows = loader.loadData();
            Records = transformer.GetTransformedData(LoadedRows);
        }

        internal void compute(int learningDataPercent, float step, float stopCondition)
        {
            LearningPercent = learningDataPercent;
            Step = step;
            StopCondition = stopCondition;
            Informations = new List<Info>();
            foreach(Record record in Records){
                
                double startAge = this.getStartAge(record);
                double leavingAge = this.getLeavingAge(record);
                if (isOutlier(record, startAge, leavingAge))
                {
                    continue;
                }
                Informations.Add(new Info(startAge, leavingAge));
            }
            int testDataSize = Informations.Count * LearningPercent / 100;
            LearningSet = Informations.GetRange(0, testDataSize - 1);
            TestSet = Informations.GetRange(testDataSize, Informations.Count - testDataSize);

            Graph.AddInfo(LearningSet, TestSet);
            Graph.GraphModel.PlotView.InvalidatePlot();

            simpleLinearRegression();

        }

        private void simpleLinearRegression()
        {
            GradientDescent gradientDescentComputer = new GradientDescent(Graph, LearningSet, Step, StopCondition, new int[] { 0, 0 });
            gradientDescentComputer.compute();
            
        }

        private bool isOutlier(Record record, double startAge, double leavingAge)
        {
            if (startAge > MAX_START_AGE)
            {
                return true;
            }
            //Debug.Print(record.RegistrationDate.Date.ToString());
            //Debug.Print(FIRST_REGISTRATION_DATE.Date.ToString());
            if (EXCLUDE_UNCOMPLETE_RECORDS && record.RegistrationDate.Date.Equals(FIRST_REGISTRATION_DATE.Date))
            {
                //Debug.Print("fff");
                return true;
            }
            else
            {
                return false;
            }
        }

        private double getLeavingAge(Record record)
        {
            DateTime endDate = record.LeavingDate == null ? DateTime.Now : (DateTime) record.LeavingDate;
            double leavingDaysTotal = endDate.Subtract(record.DateOfBirth).TotalDays;
            double leavingAge = Math.Round(leavingDaysTotal / 365, DECIMAL_DIGITS); 
            return leavingAge;
        }

        private double getStartAge(Record record)
        {
            double startDaysTotal = record.RegistrationDate.Subtract(record.DateOfBirth).TotalDays;
            double startAge = Math.Round(startDaysTotal / 365, DECIMAL_DIGITS);
            return startAge;
        }

    }
}
