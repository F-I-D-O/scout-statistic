using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKS
{
    public class Graph
    {

        const int POINT_SIZE = 2;

        private Series lastTempSeries;

    

        public Graph()
        {
            this.GraphModel = new PlotModel { Title = "28. oddíl Vločka - Závislost věku odchodu dětí z organizace na věku registrace." };
            //this.GraphModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

            //Func border = SystemExistingBorder;

            //this.GraphModel.Series.Add(new FunctionSeries(Graph.SystemExistingBorder, 4, 15, 2, "hranice dostupných dat"));


            LineSeries lineSeriesScouts = new LineSeries("hranice skautského věku");
            lineSeriesScouts.Points.Add(new DataPoint(Programe.MIN_START_AGE, Programe.SCOUT_BORDER_AGE));
            lineSeriesScouts.Points.Add(new DataPoint(Programe.MAX_START_AGE, Programe.SCOUT_BORDER_AGE));
            this.GraphModel.Series.Add(lineSeriesScouts);

            LineSeries lineSeriesRover = new LineSeries("hranice věku pro rovery a vedení");
            lineSeriesRover.Points.Add(new DataPoint(Programe.MIN_START_AGE, Programe.ROVER_BORDER_AGE));
            lineSeriesRover.Points.Add(new DataPoint(Programe.MAX_START_AGE, Programe.ROVER_BORDER_AGE));
            this.GraphModel.Series.Add(lineSeriesRover);
        }


        

        public void printRegression(double a, double b, Boolean isTemp = false, int iterationNumber = 0)
        {
            if (lastTempSeries != null)
            {
                GraphModel.Series.Remove(lastTempSeries);
            }
            Series series = new FunctionSeries(delegate(double startAge) { return a * startAge + b; }, 4, 15, 2);
            series.Title = isTemp ? "mezivýsledek " + iterationNumber.ToString() : "Regrese";
            if (isTemp)
            {
                lastTempSeries = series;
            }
            
            this.GraphModel.Series.Add(series);
        }


        private static double SystemExistingBorder(double startAge)
        {
            return startAge + 12;
        }

        public PlotModel GraphModel { get; private set; }


        public void AddInfo(List<Info> LearningSet, List<Info> TestSet)
        {
            ScatterSeries learningSetSeries = new ScatterSeries();
            foreach (Info info in LearningSet)
            {
                learningSetSeries.Points.Add(new ScatterPoint(info.StartAge, info.QuitAge));
            }
            learningSetSeries.MarkerSize = POINT_SIZE;
            GraphModel.Series.Add(learningSetSeries);

            ScatterSeries testSetSeries = new ScatterSeries();
            foreach (Info info in TestSet)
            {
                testSetSeries.Points.Add(new ScatterPoint(info.StartAge, info.QuitAge));
            }
            testSetSeries.MarkerSize = POINT_SIZE;
            GraphModel.Series.Add(testSetSeries);
            
        }
        
    }
}
