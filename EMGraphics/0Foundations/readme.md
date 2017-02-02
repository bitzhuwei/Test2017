﻿# 基础
这个文件夹里写的都是OpenGL的概念。CSharpGL所涉及的所有OpenGL知识点，都被封装为enum, struct, interface, class代码。这些知识点包含在类型的名字上；包含在方法的名字上；包含在类型的继承关系上；包含在所有的注释上。
## Shader
一个Shader就是在GPU上运行的一小段（也许并不小）类C代码。  
现代OpenGL的渲染是建立在GLSL的shader基础上的。Shader工作在OpenGL渲染管线的各自的阶段上。这些阶段如何施展全看你的shader怎么写。
## Buffer
`Buffer`及其子类型是对OpenGL的缓存对象的封装。一个`Buffer`对象代表的是GPU内存里的一个数组。
OpenGL中执行渲染的指令是`glDrawArrays()`和`glDrawElements()`以及他们的高级变种版本。如果你想从零开始学OpenGL，也许可以首先围绕这两个指令开始。
## 摄像机
摄像机(Camera)是在世界坐标系下的一个特殊的物体。  
`Camera`把模型的坐标从世界坐标系转换到camera/view/eye坐标系。
## uniform变量
`UniformVariable`封装了shader里的uniform变量（例如`uniform vec3 vPosition;`）。`UniformVariable`在`Renderer`里用于为uniform变量指定值。
## OpenGL开关
OpenGL是个状态机。`GLState`就是控制其状态的。  
例如`LineWidthState`控制线的宽度。在渲染前将线宽设置为指定的宽度，在渲染后恢复到原来的宽度。  
这可以避免忘记恢复原有状态的bug。
## Rendering
`Renderer`用Modern OpenGL(VBO+Shader)进行渲染。以`IBufferable`为模型数据，以`ShaderCode`为shader数据，以`AttributeMap`为两者之间的关联关系。可自定义开关（`GLState`）。可自定义uniform变量。
## 其他
纹理、帧缓存、查询对象等等，在你学会上述内容之后就仅仅是一些很简单的概念而已。

# Foundations
OpenGL concepts lay in this folder. All basic OpenGL knowledge included in CSharpGL are wrapped into types(enum, interface, struct, class). It exists in type's names, method's names, inheritance relationships, and all comments.
## Shader
A shader is a small(maybe not that small) piece of C-like code that executes on GPU.  
Modern OpenGL rendering is built on GLSL shader. Shaders works on their own stages in OpenGL rendering pipeline. It's all up to you how these stages work.
## Buffer
`Buffer` their subtypes wrap all kinds of buffer object in OpenGL. A buffer object represents an array in CPU memory.  
The actual rendering command in OpenGL is `glDrawArrays()` and `glDrawElements()` and their advanced versions. If you want to learn OpenGL from scratch, maybe it's a good choice to focus on these two commands.  
## Camera
Camera is a special object in world space.  
`Camera` transforms object's coordinates from world coordinate system to camera/view/eye coordiate system.
## Uniform Variable
`UniformVariable` wraps uniform variables in shader like `uniform vec3 vPosition;`. `UniformVariable` is used in `Renderer` to setup uniform variable's value.
## OpenGL switch
OpenGL works as a state machine. `GLState` controls one of states in OpenGL.  
For example, `LineWidthState` controls line's width. It sets line's width to specified value before rendering, and reset it to original value after rendering.  
This could prevent future bugs about forgetting to reset to original state.
## Rendering
`Renderer` renders a model with VBOs and shaders. `IBufferable` provides model's data. `ShaderCode` provides shader code. `AttributeMap` provides mapping relations between model data and shader's variables. Different kinds of `GLState`es and uniform variables are supported.
## Other stuff
Texture, framebuffer and query object are simple concepts after you've learnt everything metioned above.
