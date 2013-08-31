// -----------------------------------------------------------------------
// <copyright file="ImEditor.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsoEngine.Block;
using Isomites.IsoEngine.Debug;
using Isomites.IsoEngine.Items;
using Isomites.IsoEngine.World;
using Isomites.IsomiteEngine;
using Isomites.IsomiteEngine.Block;
using Isomites.IsomiteEngine.Items;
using Isomites.IsomiteEngine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Isomites.IsoEngine.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum ImEditorType
    {
        Blocks,
        TopRamps,
        BottomRamps,
        Objects
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImEditor
    {
        public ImSegmentManager ParentSegmentManager;

        private ImSegmentLocation _keyboardCursor;

        private List<ImBlockMask> _blockMasks;
        private List<ImBlockMask> _topRampMasks;
        private List<ImBlockMask> _bottomRampMasks;
        private List<ImItemType> _objects;
        private ImBlockMask _rampDirection;
        private ImBlockMask _activeBlockMask;

        private ImRenderBasic _blockHighlight;
        private ImRenderBasic _blockHighlightBlocked;
        private ImEditorType _selectedType;

        private ImBlockVertexData _previewData;

        private int _index;

        public ImEditor(ImSegmentManager parent)
        {
            ParentSegmentManager = parent;

            _blockMasks = new List<ImBlockMask>();
            _blockMasks.Add(ImBlockHelper.BlockMasks.Debug);
            _blockMasks.Add(ImBlockHelper.BlockMasks.Soil);

            _topRampMasks = new List<ImBlockMask>();
            _topRampMasks.Add(ImBlockHelper.RampBlockMasks.Top.Debug);
            _topRampMasks.Add(ImBlockHelper.RampBlockMasks.Top.Soil);

            _bottomRampMasks = new List<ImBlockMask>();
            _bottomRampMasks.Add(ImBlockHelper.RampBlockMasks.Bottom.Debug);
            _bottomRampMasks.Add(ImBlockHelper.RampBlockMasks.Bottom.Soil);

            _objects = new List<ImItemType>();
            _objects.Add(ImItemTypes.Tree);

            _index = 0;

            _selectedType = ImEditorType.Blocks;

            _keyboardCursor = new ImSegmentLocation(Vector3.Zero);

            _rampDirection = ImRampDirection.North;

            _activeBlockMask = ImBlockHelper.BlockMasks.Soil;

            _previewData = new ImBlockVertexData();
        }

        public void LoadContent(ContentManager content, Effect effect)
        {
            _blockHighlight = new ImRenderBasic(content.Load<Model>("Models/HighLight"), effect);
            _blockHighlightBlocked = new ImRenderBasic(content.Load<Model>("Models/HighLight_Blocked"), effect);
        }

        public void Update()
        {
            bool changed = false;

            // SHIFT 
            if (InputHelper.IsKeyDown(Keys.LeftShift) || InputHelper.IsKeyDown(Keys.RightShift))
            {
                if (InputHelper.IsNewKeyPress(Keys.Right))
                {
                    if (_rampDirection == ImRampDirection.North)
                    {
                        _rampDirection = ImRampDirection.East;
                    }
                    else if (_rampDirection == ImRampDirection.East)
                    {
                        _rampDirection = ImRampDirection.South;
                    }
                    else if (_rampDirection == ImRampDirection.South)
                    {
                        _rampDirection = ImRampDirection.West;
                    }
                    else if (_rampDirection == ImRampDirection.West)
                    {
                        _rampDirection = ImRampDirection.North;
                    }
                    
                    changed = true;
                }

                if (InputHelper.IsNewKeyPress(Keys.Left))
                {

                    if (_rampDirection == ImRampDirection.North)
                    {
                        _rampDirection = ImRampDirection.West;
                    }
                    else if (_rampDirection == ImRampDirection.East)
                    {
                        _rampDirection = ImRampDirection.North;
                    }
                    else if (_rampDirection == ImRampDirection.South)
                    {
                        _rampDirection = ImRampDirection.East;
                    }
                    else if (_rampDirection == ImRampDirection.West)
                    {
                        _rampDirection = ImRampDirection.South;
                    }
                    changed = true;
                }
            }
            else
            {
                if (InputHelper.IsNewKeyPress(Keys.Up))
                {
                    switch (_selectedType)
                    {
                        case ImEditorType.Blocks:
                            _selectedType = ImEditorType.BottomRamps;
                            break;
                        case ImEditorType.BottomRamps:
                            _selectedType = ImEditorType.TopRamps;
                            break;
                        case ImEditorType.TopRamps:
                            _selectedType = ImEditorType.Blocks;
                            break;
                        default:
                            _previewData = new ImBlockVertexData();
                            break;
                    }
                    changed = true;
                }

                if (InputHelper.IsNewKeyPress(Keys.Down))
                {
                    switch (_selectedType)
                    {
                        case ImEditorType.Blocks:
                            _selectedType = ImEditorType.TopRamps;
                            break;
                        case ImEditorType.BottomRamps:
                            _selectedType = ImEditorType.Blocks;
                            break;
                        case ImEditorType.TopRamps:
                            _selectedType = ImEditorType.BottomRamps;
                            break;
                        default:
                            _previewData = new ImBlockVertexData();
                            break;
                    }
                    changed = true;
                }

                if (InputHelper.IsNewKeyPress(Keys.Right))
                {
                    _index++;

                    if (_index >= CountCurrentList())
                    {
                        _index = 0;
                    }
                    changed = true;
                }

                if (InputHelper.IsNewKeyPress(Keys.Left))
                {
                    _index--;

                    if (_index < 0)
                    {
                        _index = CountCurrentList() - 1;
                    }
                    changed = true;
                }
            }

            if (changed)
            {
                switch (_selectedType)
                {
                    case ImEditorType.Blocks:
                        _activeBlockMask = _blockMasks[_index];
                        _previewData = ImBlockVertexData.GetBlockMaskVertexData(_blockMasks[_index]).Copy();
                        break;
                    case ImEditorType.BottomRamps:
                        _activeBlockMask = _bottomRampMasks[_index] | _rampDirection;
                        _previewData = ImBlockRampVertexData.GetBlockMaskVertexData(_bottomRampMasks[_index] | _rampDirection).Copy();
                        break;
                    case ImEditorType.TopRamps:
                        _activeBlockMask = _topRampMasks[_index] | _rampDirection;
                        _previewData = ImBlockRampVertexData.GetBlockMaskVertexData(_topRampMasks[_index]| _rampDirection).Copy();
                        break;
                    default:
                        _previewData = new ImBlockVertexData();
                        break;
                }
            }

            HandleHighlight();
        }

        public void HandleHighlight()
        {
            Vector3 newPosition = _keyboardCursor.WorldLocation;
            if (InputHelper.IsNewKeyPress(Keys.NumPad3))
            {
                newPosition += ImDirection.East;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad1))
            {
                newPosition += ImDirection.North;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad7))
            {
                newPosition += ImDirection.West;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad9))
            {
                newPosition += ImDirection.South;
            }
            if (InputHelper.IsNewKeyPress(Keys.Add))
            {
                newPosition += ImDirection.Up;
            }
            if (InputHelper.IsNewKeyPress(Keys.Subtract))
            {
                newPosition += ImDirection.Down;
            }

            // Following is for valid block ranges only.
            if (ParentSegmentManager.GetBlockMaskAt(new ImSegmentLocation(newPosition)) != ImBlockHelper.BlockMasks.Null)
            {
                if (newPosition != _keyboardCursor.WorldLocation)
                {
                    // check for an object?
                    int segmentX = (int) Math.Floor((double) newPosition.X/ImGlobal.SegmentSize.X);
                    int segmentZ = (int) Math.Floor((double) newPosition.Z/ImGlobal.SegmentSize.Z);

                    //_ItemsUnderCursor = ParentSegmentManager.Segments[segmentX, segmentZ].Items.FindItemsAt(newPosition);
                }

                _keyboardCursor = new ImSegmentLocation(newPosition);
                if (InputHelper.IsNewKeyPress(Keys.Delete))
                {
                    ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, ImBlockHelper.BlockMasks.Air);
                }

                if (InputHelper.IsNewKeyPress(Keys.Insert))
                {
                    ImBlockMask maskAtCursor = ParentSegmentManager.GetBlockMaskAt(_keyboardCursor);
                    switch (_selectedType)
                    {
                        case ImEditorType.Blocks:
                            ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, _activeBlockMask);
                            break;
                        case ImEditorType.BottomRamps:
                            if (ImBlockHelper.HasTopRamp(maskAtCursor) &&
                                ImBlockHelper.GetRampDirection(maskAtCursor) ==
                                _rampDirection)
                            {
                                ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, maskAtCursor | _activeBlockMask);
                            }
                            else
                            {
                                ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, _activeBlockMask);
                            }
                            break;
                        case ImEditorType.TopRamps:
                            if (ImBlockHelper.HasBottomRamp(maskAtCursor) &&
                                ImBlockHelper.GetRampDirection(maskAtCursor) ==
                                _rampDirection)
                            {
                                ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, maskAtCursor | _activeBlockMask);
                            }
                            else
                            {
                                ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, _activeBlockMask);
                            }
                            break;
                        default:
                            _previewData = new ImBlockVertexData();
                            break;
                    }
                }
            }
            else
            {
                DebugLog.Log("Can't move World Cursor to (" + newPosition.X + ", " + newPosition.Y + ", " + newPosition.Z + ") - Out of bounds", DebugMessageType.Warning);
            }
        }

        public int CountCurrentList()
        {
            switch (_selectedType)
            {
                case ImEditorType.Blocks:
                    return _blockMasks.Count;
                case ImEditorType.BottomRamps:
                    return _bottomRampMasks.Count;
                case ImEditorType.TopRamps:
                    return _topRampMasks.Count;
                case ImEditorType.Objects:
                    return _objects.Count;
                default:
                    return 0;
            }
        }

        public void DrawBlocks()
        {
            VertexPositionNormalTexture[] previewVerts =
                _previewData.GetAllVertsTranslated(_keyboardCursor.WorldLocation);
            short[] previewIndices = _previewData.GetAllIndices();


            if (previewVerts.Count() > 0)
            {
                ParentSegmentManager.Device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                    previewVerts, 0,
                    previewVerts.Count(), previewIndices, 0,
                    previewIndices.Count()/3);
            }
        }

        public void DrawItems(Camera3D camera)
        {
            ImBlockMask maskAtCursor = ParentSegmentManager.GetBlockMaskAt(_keyboardCursor);

            switch (_selectedType)
            {
                case ImEditorType.Blocks:
                    if (ParentSegmentManager.IsLocationObstructed(_keyboardCursor))
                    {
                        _blockHighlightBlocked.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                    }
                    else
                    {
                        _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                    }
                    break;
                case ImEditorType.BottomRamps:
                    if (ImBlockHelper.HasTopRamp(maskAtCursor) &&
                        ImBlockHelper.GetRampDirection(maskAtCursor) ==
                        _rampDirection)
                    {
                        _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                    }
                    else
                    {
                        if (ParentSegmentManager.IsLocationObstructed(_keyboardCursor))
                        {
                            _blockHighlightBlocked.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                        }
                        else
                        {
                            _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                        }
                    }
                    break;
                case ImEditorType.TopRamps:
                    if (ImBlockHelper.HasBottomRamp(maskAtCursor) &&
                        ImBlockHelper.GetRampDirection(maskAtCursor) ==
                        _rampDirection)
                    {
                        _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                    }
                    else
                    {
                        if (ParentSegmentManager.IsLocationObstructed(_keyboardCursor))
                        {
                            _blockHighlightBlocked.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                        }
                        else
                        {
                            _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                        }
                    }
                    break;
                default:
                    _previewData = new ImBlockVertexData();
                    break;
            }
        }

        public void Draw(GraphicsDevice device, Camera3D camera)
        {
           

            if (_previewData.GetAllVertsTranslated(Vector3.Zero).Count() > 0)
            {
                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                    _previewData.GetAllVertsTranslated(_keyboardCursor.WorldLocation), 0,
                    _previewData.GetAllVertsTranslated(Vector3.Zero).Count(), _previewData.GetAllIndices(), 0,
                    _previewData.GetAllIndices().Count()/3);
            }
        }
    }
}
