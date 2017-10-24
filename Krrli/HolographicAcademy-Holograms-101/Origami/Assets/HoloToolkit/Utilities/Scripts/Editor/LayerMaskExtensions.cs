// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text;
using UnityEngine;

namespace HoloToolkit.Unity
{
    /// <summary>
    /// Extensions for the UnityEngine.LayerMask class.
    /// </summary>
    public static class LayerMaskExtensions
    {
        public const int LayerCount = 32;

<<<<<<< Updated upstream
        private static string[] layerMaskNames;
=======
        private static string[] layerMaskNames = null;
>>>>>>> Stashed changes
        public static string[] LayerMaskNames
        {
            get
            {
                if (layerMaskNames == null)
                {
<<<<<<< Updated upstream
                    layerMaskNames = new string[LayerCount];
                    for (int layer = 0; layer < LayerCount; ++layer)
                    {
                        layerMaskNames[layer] = LayerMask.LayerToName(layer);
                    }
                }

                return layerMaskNames;
=======
                    LayerMaskExtensions.layerMaskNames = new string[LayerCount];
                    for (int layer = 0; layer < LayerCount; ++layer)
                    {
                        LayerMaskExtensions.layerMaskNames[layer] = LayerMask.LayerToName(layer);
                    }
                }

                return LayerMaskExtensions.layerMaskNames;
>>>>>>> Stashed changes
            }
        }

        public static string GetDisplayString(this LayerMask layerMask)
        {
            StringBuilder stringBuilder = null;
<<<<<<< Updated upstream
            for (int layer = 0; layer < LayerCount; ++layer)
=======
            for (int layer = 0; layer < LayerMaskExtensions.LayerCount; ++layer)
>>>>>>> Stashed changes
            {
                if ((layerMask & (1 << layer)) != 0)
                {
                    if (stringBuilder == null)
                    {
                        stringBuilder = new StringBuilder();
                    }
                    else
                    {
                        stringBuilder.Append(" | ");
                    }

<<<<<<< Updated upstream
                    stringBuilder.Append(LayerMaskNames[layer]);
=======
                    stringBuilder.Append(LayerMaskExtensions.LayerMaskNames[layer]);
>>>>>>> Stashed changes
                }
            }

            return stringBuilder == null ? "None" : stringBuilder.ToString();
        }
    }
}