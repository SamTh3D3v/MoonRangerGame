// Type: Microsoft.Xna.Framework.Graphics.GraphicsDevice
// Assembly: Microsoft.Xna.Framework.Graphics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553
// Assembly location: C:\Program Files (x86)\Microsoft XNA\XNA Game Studio\v4.0\References\Windows\x86\Microsoft.Xna.Framework.Graphics.dll

using Microsoft.Xna.Framework;
using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
  /// <summary>
  /// Performs primitive-based rendering, creates resources, handles system-level variables, adjusts gamma ramp levels, and creates shaders.
  /// </summary>
  public class GraphicsDevice : IDisposable
  {
    /// <summary>
    /// Creates an instance of this object.
    /// </summary>
    /// <param name="adapter">The display adapter.</param><param name="graphicsProfile">The graphics profile.</param><param name="presentationParameters">The presentation options.</param>
    public GraphicsDevice(GraphicsAdapter adapter, GraphicsProfile graphicsProfile, PresentationParameters presentationParameters);
    /// <summary>
    /// Specifies the window target for a presentation and presents the display with the contents of the next buffer in the sequence of back buffers owned by the GraphicsDevice.
    /// </summary>
    /// <param name="sourceRectangle">The source rectangle. If null, the entire source surface is presented. If the rectangle exceeds the source surface, the rectangle is clipped to the source surface.</param><param name="destinationRectangle">The destination rectangle, in window client coordinates. If null, the entire client area is filled. If the rectangle exceeds the destination client area, the rectangle is clipped to the destination client area.</param><param name="overrideWindowHandle">Destination window containing the client area that is the target for this presentation. If not specified, this is DeviceWindowHandle.</param>
    public void Present(Rectangle? sourceRectangle, Rectangle? destinationRectangle, IntPtr overrideWindowHandle);
    /// <summary>
    /// Presents the display with the contents of the next buffer in the sequence of back buffers owned by the GraphicsDevice.
    /// </summary>
    public void Present();
    /// <summary>
    /// Resets the presentation parameters for the current GraphicsDevice.
    /// </summary>
    public void Reset();
    /// <summary>
    /// Resets the current GraphicsDevice with the specified PresentationParameters.
    /// </summary>
    /// <param name="presentationParameters">Describes the new presentation parameters. This value cannot be null.</param>
    public void Reset(PresentationParameters presentationParameters);
    /// <summary>
    /// Resets the specified Reset with the specified presentation parameters.
    /// </summary>
    /// <param name="presentationParameters">Describes the new presentation parameters. This value cannot be null.</param><param name="graphicsAdapter">The graphics device being reset.</param>
    public void Reset(PresentationParameters presentationParameters, GraphicsAdapter graphicsAdapter);
    /// <summary>
    /// Renders a sequence of non-indexed geometric primitives of the specified type from the current set of data input streams.
    /// </summary>
    /// <param name="primitiveType">Describes the type of primitive to render.</param><param name="startVertex">Index of the first vertex to load. Beginning at startVertex, the correct number of vertices is read out of the vertex buffer.</param><param name="primitiveCount">Number of primitives to render. The primitiveCount is the number of primitives as determined by the primitive type. If it is a line list, each primitive has two vertices. If it is a triangle list, each primitive has three vertices.</param>
    public void DrawPrimitives(PrimitiveType primitiveType, int startVertex, int primitiveCount);
    /// <summary>
    /// Renders the specified geometric primitive, based on indexing into an array of vertices.
    /// </summary>
    /// <param name="primitiveType">Describes the type of primitive to render. PrimitiveType.PointList is not supported with this method.</param><param name="baseVertex">Offset to add to each vertex index in the index buffer.</param><param name="minVertexIndex">Minimum vertex index for vertices used during the call. The minVertexIndex parameter and all of the indices in the index stream are relative to the baseVertex parameter.</param><param name="numVertices">Number of vertices used during the call. The first vertex is located at index: baseVertex + minVertexIndex.</param><param name="startIndex">Location in the index array at which to start reading vertices.</param><param name="primitiveCount">Number of primitives to render. The number of vertices used is a function of primitiveCount and primitiveType.</param>
    public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount);
    /// <summary>
    /// Draws a series of instanced models.
    /// </summary>
    /// <param name="primitiveType">The primitive type.</param><param name="baseVertex">Offset to add to each vertex index in the index buffer.</param><param name="minVertexIndex">Minimum vertex index for vertices used during the call. The minVertexIndex parameter and all of the indices in the index stream are relative to the baseVertex parameter.</param><param name="numVertices">Number of vertices used during the call. The first vertex is located at index: baseVertex + minVertexIndex.</param><param name="startIndex">Location in the index array at which to start reading vertices.</param><param name="primitiveCount">Number of primitives to render. The number of vertices used is a function of primitiveCount and primitiveType.</param><param name="instanceCount">Number of primitives to render.</param>
    public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, int instanceCount);
    /// <summary>
    /// Renders indexed primitives from a 32-bit index buffer and other related input parameters.
    /// </summary>
    /// <param name="primitiveType">The primitive type.</param><param name="vertexData">The vertex data.</param><param name="vertexOffset">Offset (in vertices) from the beginning of the vertex buffer to the first vertex to draw.</param><param name="numVertices">Number of vertices to draw.</param><param name="indexData">The index data.</param><param name="indexOffset">Offset (in indices) from the beginning of the index buffer to the first index to use.</param><param name="primitiveCount">Number of primitives to render.</param>
    public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, int[] indexData, int indexOffset, int primitiveCount) where T : struct, IVertexType;
    /// <summary>
    /// Renders indexed primitives from a 16-bit index buffer and other related input parameters.
    /// </summary>
    /// <param name="primitiveType">The primitive type.</param><param name="vertexData">The vertex data.</param><param name="vertexOffset">Offset (in vertices) from the beginning of the vertex buffer to the first vertex to draw.</param><param name="numVertices">Number of vertices to draw.</param><param name="indexData">The index data.</param><param name="indexOffset">Offset (in indices) from the beginning of the index buffer to the first index to use.</param><param name="primitiveCount">Number of primitives to render.</param>
    public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, short[] indexData, int indexOffset, int primitiveCount) where T : struct, IVertexType;
    /// <summary>
    /// Renders indexed primitives from a 32-bit index buffer, a vertex declaration, and other related input parameters.
    /// </summary>
    /// <param name="primitiveType">The primitive type.</param><param name="vertexData">The vertex data.</param><param name="vertexOffset">Offset (in vertices) from the beginning of the vertex buffer to the first vertex to draw.</param><param name="numVertices">Number of vertices to draw.</param><param name="indexData">The index data.</param><param name="indexOffset">Offset (in indices) from the beginning of the index buffer to the first index to use.</param><param name="primitiveCount">Number of primitives to render.</param><param name="vertexDeclaration">The vertex declaration.</param>
    public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, int[] indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct;
    /// <summary>
    /// Renders indexed primitives from a 16-bit index buffer, a vertex declaration, and other related input parameters.
    /// </summary>
    /// <param name="primitiveType">The primitive type.</param><param name="vertexData">The vertex data.</param><param name="vertexOffset">Offset (in vertices) from the beginning of the vertex buffer to the first vertex to draw.</param><param name="numVertices">Number of vertices to draw.</param><param name="indexData">The index data.</param><param name="indexOffset">Offset (in indices) from the beginning of the index buffer to the first index to use.</param><param name="primitiveCount">Number of primitives to render.</param><param name="vertexDeclaration">The vertex declaration.</param>
    public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, short[] indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct;
    /// <summary>
    /// Draws primitives.
    /// </summary>
    /// <param name="primitiveType">Describes the type of primitive to render.</param><param name="vertexData">The vertex data.</param><param name="vertexOffset">Offset (in vertices) from the beginning of the buffer to start reading data.</param><param name="primitiveCount">Number of primitives to render.</param>
    public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount) where T : struct, IVertexType;
    /// <summary>
    /// Draws primitives.
    /// </summary>
    /// <param name="primitiveType">Describes the type of primitive to render.</param><param name="vertexData">The vertex data.</param><param name="vertexOffset">Offset (in vertices) from the beginning of the buffer to start reading data.</param><param name="primitiveCount">Number of primitives to render.</param><param name="vertexDeclaration">The vertex declaration, which defines per-vertex data.</param>
    public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct;
    /// <summary>
    /// Clears resource buffers.
    /// </summary>
    /// <param name="options">Options for clearing a buffer.</param><param name="color">Set this four-component color value in the buffer.</param><param name="depth">Set this depth value in the buffer.</param><param name="stencil">Set this stencil value in the buffer.</param>
    public void Clear(ClearOptions options, Vector4 color, float depth, int stencil);
    /// <summary>
    /// Clears resource buffers.
    /// </summary>
    /// <param name="options">Options for clearing a buffer.</param><param name="color">Set this color value in the buffer.</param><param name="depth">Set this depth value in the buffer.</param><param name="stencil">Set this stencil value in the buffer.</param>
    public void Clear(ClearOptions options, Color color, float depth, int stencil);
    /// <summary>
    /// Clears resource buffers.
    /// </summary>
    /// <param name="color">Set this color value in all buffers.</param>
    public void Clear(Color color);
    /// <summary>
    /// Sets an array of render targets.
    /// </summary>
    /// <param name="renderTargets">[ParamArrayAttribute] An array of render targets.</param>
    public void SetRenderTargets(params RenderTargetBinding[] renderTargets);
    /// <summary>
    /// Sets a new render target for this GraphicsDevice.
    /// </summary>
    /// <param name="renderTarget">A new render target for the device, or null to set the device render target to the back buffer of the device.</param><param name="cubeMapFace">The cube map face type.</param>
    public void SetRenderTarget(RenderTargetCube renderTarget, CubeMapFace cubeMapFace);
    /// <summary>
    /// Sets a new render target for this GraphicsDevice.
    /// </summary>
    /// <param name="renderTarget">A new render target for the device, or null to set the device render target to the back buffer of the device.</param>
    public void SetRenderTarget(RenderTarget2D renderTarget);
    /// <summary>
    /// Gets a render target surface.
    /// </summary>
    public RenderTargetBinding[] GetRenderTargets();
    /// <summary>
    /// Gets the contents of the back buffer.
    /// </summary>
    /// <param name="rect">The section of the back buffer to copy. null indicates the data will be copied from the entire back buffer.</param><param name="data">Array of data.</param><param name="startIndex">The first element to use.</param><param name="elementCount">The number of elements to use.</param>
    public void GetBackBufferData<T>(Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct;
    /// <summary>
    /// Gets the contents of the back buffer.
    /// </summary>
    /// <param name="data">Array of data.</param><param name="startIndex">The first element to use.</param><param name="elementCount">The number of elements to use.</param>
    public void GetBackBufferData<T>(T[] data, int startIndex, int elementCount) where T : struct;
    /// <summary>
    /// Gets the contents of the back buffer.
    /// </summary>
    /// <param name="data">Array of data.</param>
    public void GetBackBufferData<T>(T[] data) where T : struct;
    /// <summary>
    /// Gets the vertex buffers.
    /// </summary>
    public VertexBufferBinding[] GetVertexBuffers();
    /// <summary>
    /// Sets or binds a vertex buffer to the device.
    /// </summary>
    /// <param name="vertexBuffer">A vertex buffer.</param><param name="vertexOffset">The offset (in vertices) from the beginning of the buffer.</param>
    public void SetVertexBuffer(VertexBuffer vertexBuffer, int vertexOffset);
    /// <summary>
    /// Sets or binds a vertex buffer to the device.
    /// </summary>
    /// <param name="vertexBuffer">A vertex buffer.</param>
    public void SetVertexBuffer(VertexBuffer vertexBuffer);
    /// <summary>
    /// Sets the vertex buffers.
    /// </summary>
    /// <param name="vertexBuffers">[ParamArrayAttribute] An array of vertex buffers.</param>
    public void SetVertexBuffers(params VertexBufferBinding[] vertexBuffers);
    /// <summary>
    /// Immediately releases the unmanaged resources used by this object.
    /// </summary>
    /// <param name="disposing">[MarshalAsAttribute(U1)] true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1);
    /// <summary>
    /// Immediately releases the unmanaged resources used by this object.
    /// </summary>
    public override sealed void Dispose();
    /// <summary>
    /// Allows this object to attempt to free resources and perform other cleanup operations before garbage collection reclaims the object.
    /// </summary>
    ~GraphicsDevice();
    /// <summary>
    /// Gets a value that indicates whether the object is disposed.
    /// </summary>
    public bool IsDisposed { get; }
    /// <summary>
    /// Gets or sets the rectangle used for scissor testing. By default, the size matches the render target size.
    /// </summary>
    public Rectangle ScissorRectangle { get; set; }
    /// <summary>
    /// Gets or sets index data. The default value is null.
    /// </summary>
    public IndexBuffer Indices { get; set; }
    /// <summary>
    /// Gets or sets a viewport identifying the portion of the render target to receive draw calls.  Reference page contains code sample.
    /// </summary>
    public Viewport Viewport { get; set; }
    /// <summary>
    /// Retrieves the display mode's spatial resolution, color resolution, and refresh frequency.  Reference page contains code sample.
    /// </summary>
    public DisplayMode DisplayMode { get; }
    /// <summary>
    /// Retrieves the status of the device.
    /// </summary>
    public GraphicsDeviceStatus GraphicsDeviceStatus { get; }
    /// <summary>
    /// Gets the graphics profile. The default value is GraphicsProfile.Reach.
    /// </summary>
    public GraphicsProfile GraphicsProfile { get; }
    /// <summary>
    /// Gets the graphics adapter.
    /// </summary>
    public GraphicsAdapter Adapter { get; }
    /// <summary>
    /// Gets the presentation parameters associated with this graphics device.
    /// </summary>
    public PresentationParameters PresentationParameters { get; }
    /// <summary>
    /// Gets or sets rasterizer state. The default value is RasterizerState.CullCounterClockwise.
    /// </summary>
    public RasterizerState RasterizerState { get; set; }
    /// <summary>
    /// Gets or sets a reference value for stencil testing. The default value is zero.
    /// </summary>
    public int ReferenceStencil { get; set; }
    /// <summary>
    /// Gets or sets a system-defined instance of a depth-stencil state object. The default value is DepthStencilState.Default.
    /// </summary>
    public DepthStencilState DepthStencilState { get; set; }
    /// <summary>
    /// Gets or sets a bitmask controlling modification of the samples in a multisample render target. The default value is -1 (0xffffffff).
    /// </summary>
    public int MultiSampleMask { get; set; }
    /// <summary>
    /// Gets or sets the color used for a constant-blend factor during alpha blending. The default value is Color.White.
    /// </summary>
    public Color BlendFactor { get; set; }
    /// <summary>
    /// Gets or sets a system-defined instance of a blend state object initialized for alpha blending. The default value is BlendState.Opaque.
    /// </summary>
    public BlendState BlendState { get; set; }
    /// <summary>
    /// Gets the collection of vertex textures that support texture lookup in the vertex shader using the texldl statement. The vertex engine contains four texture sampler stages.
    /// </summary>
    public TextureCollection VertexTextures { get; }
    /// <summary>
    /// Returns the collection of textures that have been assigned to the texture stages of the device.  Reference page contains code sample.
    /// </summary>
    public TextureCollection Textures { get; }
    /// <summary>
    /// Gets the collection of vertex sampler states.
    /// </summary>
    public SamplerStateCollection VertexSamplerStates { get; }
    /// <summary>
    /// Retrieves a collection of SamplerState objects for the current GraphicsDevice.
    /// </summary>
    public SamplerStateCollection SamplerStates { get; }
    /// <summary>
    /// Occurs when Dispose is called or when this object is finalized and collected by the garbage collector of the Microsoft .NET common language runtime.  Reference page contains code sample.
    /// </summary>
    /// <param name=""/>
    public event EventHandler<EventArgs> Disposing;
    protected void raise_Disposing(object value0, EventArgs value1);
    /// <summary>
    /// Occurs when a resource is destroyed.
    /// </summary>
    /// <param name="">The event data.</param>
    public event EventHandler<ResourceDestroyedEventArgs> ResourceDestroyed;
    protected void raise_ResourceDestroyed(object value0, ResourceDestroyedEventArgs value1);
    /// <summary>
    /// Occurs when a resource is created.
    /// </summary>
    /// <param name="">The event data.</param>
    public event EventHandler<ResourceCreatedEventArgs> ResourceCreated;
    protected void raise_ResourceCreated(object value0, ResourceCreatedEventArgs value1);
    /// <summary>
    /// Occurs when a GraphicsDevice is about to be lost (for example, immediately before a reset).  Reference page contains code sample.
    /// </summary>
    /// <param name=""/>
    public event EventHandler<EventArgs> DeviceLost;
    protected void raise_DeviceLost(object value0, EventArgs value1);
    /// <summary>
    /// Occurs after a GraphicsDevice is reset, allowing an application to recreate all resources.
    /// </summary>
    /// <param name=""/>
    public event EventHandler<EventArgs> DeviceReset;
    protected void raise_DeviceReset(object value0, EventArgs value1);
    /// <summary>
    /// Occurs when a GraphicsDevice is resetting, allowing the application to cancel the default handling of the reset.  Reference page contains code sample.
    /// </summary>
    /// <param name=""/>
    public event EventHandler<EventArgs> DeviceResetting;
    protected void raise_DeviceResetting(object value0, EventArgs value1);
  }
}
