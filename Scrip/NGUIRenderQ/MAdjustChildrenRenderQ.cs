using System;
using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scrip.NGUIRenderQ
{
    
    [RequireComponent(typeof(UITexture))]
    class MAdjustChildrenRenderQ:MonoBehaviour
    {
        /// <summary>
        /// use share materials
        /// </summary>
        public bool useShareMaterial = false;
        
        /// <summary>
        /// use bind texture
        /// </summary>
        private UITexture mTexture;

        /// <summary>
        /// if depth = 0 ,use the default depth
        /// </summary>
        private int defaultDepth = 10;

        private List<Renderer> mChildrenRenders;
        private int mRenderQ = 0;

        /// <summary>
        /// width and height
        /// </summary>
        private Vector2 mwh = Vector2.zero;


        private int lastCount = 0;
        private Vector2 lastWH = Vector2.zero;
        /// <summary>
        /// get my texture
        /// </summary>
        private UITexture getMTexture
        {
            get
            {
                if (mTexture == null)
                {
                    mTexture = GetComponent<UITexture>();
                    
                }
                return mTexture;
            }
        }

        public int Depth
        {
            get { return getMTexture.depth;}
            set { getMTexture.depth = value; }
        }

        public Vector2 WH
        {
            get
            {
                mwh = new Vector2(transform.localScale.x, transform.localScale.y);
                return mwh;
            }
            set
            {
                mwh = value;
            }
        }


        //use this for initialization
        void Awake()
        {
            if(getMTexture.depth == 0)
            {
                getMTexture.depth = defaultDepth;
            }
        }

        void Start()
        {
            SetTexture();
        }

        public void Refresh()
        {
            GetChildrenRenders();
        }

        /// <summary>
        /// get target's shader in childer
        /// </summary>
        private void GetChildrenRenders()
        {
            if (getMTexture != null)
            {
                mChildrenRenders = new List<Renderer>(getMTexture.GetComponentsInChildren<Renderer>(true));
            }

            for (int i = 0;i<mChildrenRenders.Count;i++)
            {
                Renderer r = mChildrenRenders[i];
                if(r.GetComponent<UIWidget>()!= null)
                {
                    mChildrenRenders.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// set target's texture
        /// </summary>
        private void SetTexture()
        {
            getMTexture.mainTexture = new Texture2D(1, 1);
            if(mwh == Vector2.zero)
            {
                mwh = new Vector2(getMTexture.width, getMTexture.height);
            }
        }

        /// <summary>
        /// update my render list renderQ
        /// </summary>
        /// <returns></returns>
        bool GetRenderQ()
        {
            if (getMTexture.drawCall == null) return false;
            {

                if (mChildrenRenders == null || mChildrenRenders.Count == 0)
                    GetChildrenRenders();

                mRenderQ = getMTexture.drawCall.renderQueue;

                return true;
            }

        }

        /// <summary>
        /// set child'object renderQ
        /// </summary>
        void AdjustChildrenRenderQ()
        {
            if (mChildrenRenders.Count> 0)
            {
                foreach(Renderer r in mChildrenRenders)
                {
                    if(r == null)
                    {
                        mChildrenRenders.Clear();
                        return;
                    }

                    Material material = useShareMaterial ? r.sharedMaterial : r.material;
                    material.renderQueue = mRenderQ;
                }
            }
        }

        /// <summary>
        /// 因为SkeletonRenderer中170行附近动态修改了sharedMaterial,重置了renderQ,所以需要在重置之后重新设置renderQ
        /// </summary>
        void LateUpdate()
        {
            bool hasDrawCall = GetRenderQ();
            if (hasDrawCall)
            {
                UpWH();
                AdjustChildrenRenderQ();
            }
        }

        /// <summary>
        /// update width and height
        /// </summary>
        private void UpWH()
        {
            int count = getMTexture.transform.childCount;
            if (lastCount == count) return;
            else lastCount = count;

            if (mwh != lastWH)
            {
                lastWH = mwh;
                for (int i = 0;i<count;i++)
                {
                    Transform trans = getMTexture.transform.GetChild(i);
                    trans.localScale = mwh;
                }
            }
        }   
    }
}
