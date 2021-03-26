using UnityEngine;
using System.Collections;
using ChartAndGraph;
using EasyUIAnimator;

public class BarChartFeed : MonoBehaviour {

    public CanvasBarChart bar;
	void Start () {
    }
    public string catValue;
    public void AddValue()
    {
       // if(bar != null)
        {
            switch(catValue)
            {
                case "2":
                    bar.DataSource.SetValue("2", "group1", bar.DataSource.GetValue("2", "group1") +1);
                    break;
                case "3":
                    bar.DataSource.SetValue("3", "group1", bar.DataSource.GetValue("3", "group1") + 1);
                    break;
                case "4":
                    bar.DataSource.SetValue("4", "group1", bar.DataSource.GetValue("4", "group1") + 1);
                    break;
                case "5":
                    bar.DataSource.SetValue("5", "group1", bar.DataSource.GetValue("5", "group1") + 1);
                    break;
                case "6":
                    bar.DataSource.SetValue("6", "group1", bar.DataSource.GetValue("6", "group1") + 1);
                    break;
            }


        }
    }

    public RectTransform target1,target2;
    public RectTransform dice1;
    public float timeV = 1.5f;

    public void getupdate()
    {
        UIAnimator.MoveTo(dice1, UIAnimator.GetCenter(target1), timeV).Play();
    }


}
