using UnityEngine;
using ChartAndGraph;

public class GraphChartFeed : MonoBehaviour
{
	void Start ()
    {
        GraphChartBase graph = GetComponent<GraphChartBase>();
        if (graph != null)
        {
            graph.DataSource.StartBatch();
            graph.DataSource.ClearCategory("Year 1");
            graph.DataSource.ClearAndMakeBezierCurve("Year 2");
            for (int i = 0; i <30; i++)
            {
                graph.DataSource.AddPointToCategory("Year 1", Random.value*10f,Random.value*10f + 20f);
                if (i == 0)
                {
                    graph.DataSource.SetCurveInitialPoint("Year 2", 0f, Random.value * 10f + 10f);
                }
                else
                {
                    graph.DataSource.AddLinearCurveToCategory("Year 2", new DoubleVector2(i * 10f / 30f, Random.value * 10f + 10f));
                }
            }

            graph.DataSource.MakeCurveCategorySmooth("Year 2");
            graph.DataSource.EndBatch();
        }
    }
}
