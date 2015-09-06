using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TKS
{
	class GradientDescent
	{

		private Info[] data;

		private int[] errorVector;

		private double step;

        private double stopCondition;

		private int[] initValues;

		private Graph graph;

		public double A {get; private set;}

		public double B { get; private set; }

        public GradientDescent(Graph graph, List<Info> data, double step, double stopCondition, int[] initValues)
		{
			this.data = data.ToArray();
			this.step = step;
            this.stopCondition = stopCondition;
			this.initValues = initValues;
			this.graph = graph;
		}

		private Boolean converged(int iteration, double diff)
		{
            //return iteration > 20 ? true : false;
            return diff < stopCondition ? true : false;
		}

		public async Task compute()
		{
			int size = data.Length;
			A = initValues[0];
			B = initValues[1];
			double aTemp, bTemp, aOld, bOld, diff;
			int iteration = 0;
			do
			{
                iteration++;
				aTemp = 0;
				bTemp = 0;
				foreach (Info info in data)
				{
			   
					aTemp += (A * info.StartAge + B - info.QuitAge) * info.StartAge;
					bTemp += (A * info.StartAge + B - info.QuitAge);
				}

                aOld = A;
                bOld = B;
				A = A - step * aTemp / size;
				B = B - step * bTemp / size;

				graph.printRegression(A, B, true, iteration);
				//Application.Current.Dispatcher.InvokeAsync(() => 
				//{
				//    graph.GraphModel.PlotView.InvalidatePlot(true);
				//});
				graph.GraphModel.PlotView.InvalidatePlot();

                diff = Math.Max(Math.Abs(A - aOld), Math.Abs(B - bOld));

				await Task.Delay(500);

            } while (!converged(iteration, diff));
            graph.printRegression(A, B);
		}
	}
}
