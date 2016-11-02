using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
// ================================
//* 功能描述：Bezier  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/6/29 15:21:52
// ================================
namespace Assets.JackCheng.BezierCurve
{
    public class BezierMono : MonoBehaviour
    {
        public Transform[] trans;

        public int PointCount = 100;

        private List<GameObject> listObjs;

        private ThreeBezier bezier;


        void Start()
        {
            bezier = new ThreeBezier();
            listObjs = new List<GameObject>();
            for (int i = 0; i < PointCount; i++)
            {
                GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                o.transform.localScale = Vector3.one;
                listObjs.Add(o);
            }

            


        }

        void Update()
        {
            for (int i = 0; i < PointCount; i++)
            {
                listObjs[i].transform.position = bezier.Drall((i + 1) / (float)PointCount, trans);
            }
        }
    }
    /// <summary>
    /// 线性贝塞尔曲线
    /// </summary>
    public class LineBezier
    {
        //B(t) = P0 +     t(P1 – P0) = (1-t) P0 + tP1 ,     0 < t < 1
        public Vector3 Drall(float t, Vector3 _start, Vector3 _end)
        {
            Vector3 val = _start + t * (_end - _start);

            return val;
        }
    }
    /// <summary>
    /// 二次贝塞尔曲线
    /// </summary>
    public class DoubleBezier
    {

        //B(t) =     (1-t) BP0,P1(t) + t BP1,P2(t), 0 < t < 1
        //B(t) =     (1-t) [(1-t) P0 + tP1] + t [(1-t) P1 +     tP2] , 0 < t < 1
        //重新整理的公式如下：
        //B(t) =     (1-t)2P0 + 2(1-t)tP1 + t2P2 ,     0 < t < 1

        public DoubleBezier()
        {
            lb = new LineBezier[2];
            lb[0] = new LineBezier();
            lb[1] = new LineBezier();
        }


        private LineBezier[] lb;

        public Vector3 Drall(float t, Vector3 _start, Vector3 _center, Vector3 _end)
        {
            Vector3 lbStart = lb[0].Drall(t, _start, _center);
            Vector3 lbEnd = lb[1].Drall(t, _center, _end);
            Vector3 val = lbStart + (lbEnd - lbStart) * t;

            return val;
        }
    }
    /// <summary>
    /// 三次贝塞尔曲线
    /// </summary>
    public class ThreeBezier
    {
        private DoubleBezier[] db;
        public ThreeBezier()
        {
            db = new DoubleBezier[2];
            db[0] = new DoubleBezier();
            db[1] = new DoubleBezier();

        }

        public Vector3 Drall(float t, Transform[] p)
        {
            Vector3 dbStart = db[0].Drall(t, p[0].position, p[1].position, p[2].position);
            Vector3 dbEnd = db[1].Drall(t, p[1].position, p[2].position, p[3].position);

            Vector3 val = dbStart + t * (dbEnd - dbStart);
            return val;
        }
    }


}
