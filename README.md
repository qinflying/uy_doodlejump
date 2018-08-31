# uy_doodlejump
Unity learn: doodle jump



#### 1.分相机渲染

a.主摄像机不看Water层

b.背景相机看Water层（背景设置为Water层）

```c#
/*Culling Mask：用于来设定是否剔除处于某一层的对象。Unity场景中的每一个对象，都被分配了一个层，默认为“default”层。打开层级管理器可以看到初始状态下分配了8个层，即0-7层是已经被U3D使用，而”default”处于第0层。
摄像机的Culling Mask的可选项就是这些被使用的层，外加两个完全选项Everthing和Nothing，摄像机Culling Mask的默认选择是Everything，即不剔除任何层，这个时候所有的层也都被选中。
*/
```

c.主摄像机Clear Flags 选择Depth only(为了2个相机的看到的内容融合？)

Clear Flags：确定摄像机屏幕的那一部分被清除；有一下几种选项。

| 选项         | 解释                                                         |
| ------------ | ------------------------------------------------------------ |
| Skybox(默认) | 屏幕的任何空白部分都会显示摄像机的天空盒，如当前摄像机没有设定天空盒，它会默认使用渲染设置中(Eidt->Render Settings)的天空盒。如默认渲染设置中也没有设定天空盒，它会退而使用背景色。可以选择是否将一个天空盒组件添加到摄像机。 |
| Solid Color  | 屏幕的任何空白部分都会显示当前摄像机的背景色。               |
| Depth Only   | 如果想要绘制一个玩家的枪而不让它在环境中不被裁剪，可以设定一个深度为0的相机来绘制环境，同时另一个深度为1的相机单独绘制这个武器。武器相机的清除标志应该设置成Depth Only。这会保持环境的图形显示在屏幕上，但是丢弃所有关于每个对象处于3D空间什么位置的信息。当枪被绘制的时候，被绘制出来的不透明部分会完全覆盖现有的被绘制的环境图像，而不管枪离墙壁多近。 |
| Don‘t clear  | 不会清除颜色和深度缓存；这会导致下一帧会在上一帧的结果上进行绘制。 |





#### 2.贴合屏幕宽度计算
Unity2D 正交相机的纵向长度**height＝size*2**

![](./README/cameracal.png)



#### 3.Rigidbody刚体运动相关(3D和2D）

a.velocity:

```
瞬间给物体一个恒定的速度，将物体提升至该速度。
```

b.AddForce:

```
添加一个力到刚体。作为结果刚体将开始移动。
```

c.ForceMode:

```
 刚体运动速度的计算公式是：f•t=m•v
 ForceMode.Force：给物体添加一个持续的力并使用其质量。
 ForceMode.Acceleration：给物体添加一个持续的加速度，但是忽略其质量。
 	即无论设置的质量为多少，都采用默认质量1
 ForceMode.Impulse：给物体添加一个瞬间的力并使用其质量
 ForceMode.VelocityChange：给物体添加一个瞬间的加速度，但是忽略其质量
```



#### 4.按键控制

角色左右移动

a.Vector3.MoveTowards进行插值移动

b.翻转：使用localScale中x参数正负值翻转

c.Input.GetKey获得持续按键状态

d.Time.time?

e.翻转时的偏移问题，重心不对称;编辑图片的中心SpriteEditor