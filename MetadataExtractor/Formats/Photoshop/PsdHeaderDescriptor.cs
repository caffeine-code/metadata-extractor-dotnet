/*
 * Copyright 2002-2015 Drew Noakes
 *
 *    Modified by Yakov Danilov <yakodani@gmail.com> for Imazen LLC (Ported from Java to C#)
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * More information about this project is available at:
 *
 *    https://drewnoakes.com/code/exif/
 *    https://github.com/drewnoakes/metadata-extractor
 */

using System;
using JetBrains.Annotations;

namespace MetadataExtractor.Formats.Photoshop
{
    /// <author>Drew Noakes https://drewnoakes.com</author>
    public sealed class PsdHeaderDescriptor : TagDescriptor<PsdHeaderDirectory>
    {
        public PsdHeaderDescriptor([NotNull] PsdHeaderDirectory directory)
            : base(directory)
        {
        }

        public override string GetDescription(int tagType)
        {
            switch (tagType)
            {
                case PsdHeaderDirectory.TagChannelCount:
                {
                    return GetChannelCountDescription();
                }

                case PsdHeaderDirectory.TagBitsPerChannel:
                {
                    return GetBitsPerChannelDescription();
                }

                case PsdHeaderDirectory.TagColorMode:
                {
                    return GetColorModeDescription();
                }

                case PsdHeaderDirectory.TagImageHeight:
                {
                    return GetImageHeightDescription();
                }

                case PsdHeaderDirectory.TagImageWidth:
                {
                    return GetImageWidthDescription();
                }

                default:
                {
                    return base.GetDescription(tagType);
                }
            }
        }

        [CanBeNull]
        public string GetChannelCountDescription()
        {
            try
            {
                // Supported range is 1 to 56.
                var value = Directory.GetInteger(PsdHeaderDirectory.TagChannelCount);
                if (value == null)
                {
                    return null;
                }
                return value + " channel" + (value == 1 ? string.Empty : "s");
            }
            catch (Exception)
            {
                return null;
            }
        }

        [CanBeNull]
        public string GetBitsPerChannelDescription()
        {
            try
            {
                // Supported values are 1, 8, 16 and 32.
                var value = Directory.GetInteger(PsdHeaderDirectory.TagBitsPerChannel);
                if (value == null)
                {
                    return null;
                }
                return value + " bit" + (value == 1 ? string.Empty : "s") + " per channel";
            }
            catch (Exception)
            {
                return null;
            }
        }

        [CanBeNull]
        public string GetColorModeDescription()
        {
            // Bitmap = 0; Grayscale = 1; Indexed = 2; RGB = 3; CMYK = 4; Multichannel = 7; Duotone = 8; Lab = 9
            try
            {
                var value = Directory.GetInteger(PsdHeaderDirectory.TagColorMode);
                if (value == null)
                {
                    return null;
                }
                switch (value)
                {
                    case 0:
                    {
                        return "Bitmap";
                    }

                    case 1:
                    {
                        return "Grayscale";
                    }

                    case 2:
                    {
                        return "Indexed";
                    }

                    case 3:
                    {
                        return "RGB";
                    }

                    case 4:
                    {
                        return "CMYK";
                    }

                    case 7:
                    {
                        return "Multichannel";
                    }

                    case 8:
                    {
                        return "Duotone";
                    }

                    case 9:
                    {
                        return "Lab";
                    }

                    default:
                    {
                        return "Unknown color mode (" + value + ")";
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [CanBeNull]
        public string GetImageHeightDescription()
        {
            try
            {
                var value = Directory.GetInteger(PsdHeaderDirectory.TagImageHeight);
                if (value == null)
                {
                    return null;
                }
                return value + " pixel" + (value == 1 ? string.Empty : "s");
            }
            catch (Exception)
            {
                return null;
            }
        }

        [CanBeNull]
        public string GetImageWidthDescription()
        {
            try
            {
                var value = Directory.GetInteger(PsdHeaderDirectory.TagImageWidth);
                if (value == null)
                {
                    return null;
                }
                return value + " pixel" + (value == 1 ? string.Empty : "s");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}