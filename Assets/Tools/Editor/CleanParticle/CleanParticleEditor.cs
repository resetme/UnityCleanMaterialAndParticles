using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UnityUtils
{
    public class CleanParticleEditor : Editor
    {

        [MenuItem("Assets/Tools/Clean Effect Prefab")]
        private static void CleanParticlesEffect()
        {
            Object[] objs = Selection.gameObjects;

            foreach (var obj in objs)
            {
                CleanParticlesEffect(AssetDatabase.GetAssetPath(obj));
            }
        }

        private static void CleanParticlesEffect(string path)
        {
            GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            ParticleSystem[] pas = go.GetComponentsInChildren<ParticleSystem>();
            ParticleSystemRenderer[] res = go.GetComponentsInChildren<ParticleSystemRenderer>();

            bool haveChange = false;
            string status = String.Empty;

            status += go.name;

            if (pas.Length > 0)
            {
                foreach (var pa in pas)
                {
                    status += " - " + pa.name + " - <color=green>";

                    ParticleSystemRenderer psr = pa.GetComponentInChildren<ParticleSystemRenderer>();

                    if (pa.trails.enabled == false)
                    {
                        //Clean Trail Render
                        if (psr.trailMaterial != null)
                        {
                            psr.trailMaterial = null;
                            status += "Delete Trail Material, ";
                            haveChange = true;
                        }
                    }

                    //Delete Default Material
                    if (psr.enabled == false)
                    {
                        if (psr.sharedMaterial != null)
                        {
                            if (psr.sharedMaterial.name.Contains("Default-Particle"))
                            {
                                psr.sharedMaterial = null;
                                status += "Delete Default Particle, ";
                                haveChange = true;
                            }
                        }
                    }

                    //Clean Mesh
                    if (psr.renderMode != ParticleSystemRenderMode.Mesh)
                    {
                        if (psr.mesh == null)
                        {
                            status +=  CreateMissingMesh(psr, out haveChange);
                        }
                        else if (psr.mesh.name != string.Empty)
                        {
                            status +=  CreateMissingMesh(psr, out haveChange);
                        }
                    }

                    status += "</color>";

                }

                if (haveChange)
                {
                    AssetDatabase.SaveAssets();
                    Debug.Log(status + ": Change saved", go);
                }
                else
                {
                    Debug.Log("<color=red> No need to Optimize </color>");
                }
            }
            else
            {
                Debug.Log("<color=red> There is no particle  component </color>");
            }

            if (haveChange)
            {
                AssetDatabase.Refresh();
            }
        }

        private static  string CreateMissingMesh(ParticleSystemRenderer psr, out bool haveChange)
        {
             
            Mesh missingMesh = new Mesh();
            psr.mesh = missingMesh;
            EditorUtility.SetDirty(psr);

            haveChange = true;
            
            return "Force mismatch.";

        }
    }
}