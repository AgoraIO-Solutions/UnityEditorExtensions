﻿using System.Net;

namespace PlayFab.Editor
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;

    public class EditorCoroutine
    {
        public static EditorCoroutine start(IEnumerator _routine)
        {
            EditorCoroutine coroutine = new EditorCoroutine(_routine);
            coroutine.start();
            return coroutine;
        }

        public static EditorCoroutine start(IEnumerator _routine, WWW www)
        {
            EditorCoroutine coroutine = new EditorCoroutine(_routine);
            _www = www;
            coroutine.start();
            return coroutine;
        }


        readonly IEnumerator routine;
        private static WWW _www;

        EditorCoroutine(IEnumerator _routine)
        {
            routine = _routine;
        }

        void start()
        {
            //Debug.Log("start");
            EditorApplication.update += update;
        }
        public void stop()
        {
            //Debug.Log("stop");
            EditorApplication.update -= update;
        }

        void update()
        {
            if (_www != null)
            {
                if (_www.isDone)
                {
                    if (!routine.MoveNext())
                    {
                        stop();
                    }
                }
            }
            else
            {
                /* NOTE: no need to try/catch MoveNext,
			     * if an IEnumerator throws its next iteration returns false.
			     * Also, Unity probably catches when calling EditorApplication.update.
			     */
                //Debug.Log("update");
                if (!routine.MoveNext())
                {
                    stop();
                }
            }
        }
    }
}
