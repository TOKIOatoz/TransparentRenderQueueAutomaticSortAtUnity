# TransparentRenderQueueAutomaticSortAtUnity
This program compares the reference positions of MainCamera and each Object, and updates the value of RenderQueue for each Update.
This source code can be used in Unity.  
Add this component to the appropriate GameObject and enter the number of transparent objects in MeshObjects.  
Then select a transparent object in MeshObjects, select a reference object for the position in ReferenceObjects, and enter the offset from the reference in MeshObjectOffset.  
If the ReferenceObject is None, the ReferenceObject is assigned to the Object selected by MeshObject.  

Operation check >>　HDRP/Lit,　HDRP/Hair

For example  
When setting the hair,

![SampleSetting](https://user-images.githubusercontent.com/46534552/121824140-dbf7ec00-cce4-11eb-9438-1697401d834c.png)

<pre>
MeshObjects = 4  
Element0  
    MeshObject = HairBack  
    ReferenceObject = HairBone  
    ReferenceObjectOffset = (0, 0, -0.05)  
Element1  
    MeshObject = HairBangs  
    ReferenceObject = HairBone  
    ReferenceObjectOffset = (0, 0, 0.05)  
Element2  
    MeshObject = HairFront  
    ReferenceObject = HairBone  
    ReferenceObjectOffset = (0, 0, 0.025)  
Element3  
    MeshObject = HairBack  
    ReferenceObject = HairBone  
    ReferenceObjectOffset = (0, 0, -0.025)  
</pre>

MainCameraとObject毎の基準の位置を比較してRenderQueueの値をUpdate毎に更新するプログラムです．  
このソースコードはUnityで使うことができます．  
適当なGameObjectにこのコンポーネントを加え，MeshObjectsにTransparentのオブジェクトの数を入力してください．  
その後，表示されたMeshObjectにTransparentのオブジェクトを選択し，ReferenceObjectに位置の基準となるObjectを選択し，MeshObjectOffsetに基準からのずれを入れてください．  
ReferenceObjectがNoneである場合，ReferenceObjectにMeshObjectで選択したObjectが代入されます．  

設定例は上記を参照してください．
髪のBoneを基準に後ろ髪，前髪，前の方の横の髪，後ろの方の横の髪のObjectを設定しています．

動作確認はHDRP/Lit及びHDRP/Hairで行いました．
