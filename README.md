# uy_doodlejump
Unity learn: doodle jump



#### 1.分相机渲染

a.主摄像机不看Water层

b.背景相机看Water层（背景设置为Water层）

c.主摄像机Clear Flags 选择Depth only(为了2个相机的看到的内容融合？)



#### 2.贴合屏幕宽度计算

![](./README/cameracal.png)



#### 3.Rigidbody2D刚体运动相关

a.AddForce方法

b.ForceMode2D枚举



#### 4.按键控制

角色左右移动

a.Vector3.MoveTowards进行插值移动

b.翻转：使用localScale中x参数正负值翻转

c.Input.GetKey获得持续按键状态

d.Time.time?

e.翻转时的偏移问题，重心不对称;编辑图片的中心SpriteEditor