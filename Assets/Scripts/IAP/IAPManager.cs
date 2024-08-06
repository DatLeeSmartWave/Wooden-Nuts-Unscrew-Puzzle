using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using System;
using TMPro;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private List<ItemIAP> listItems = new List<ItemIAP>();
    public GameObject[] listTextPrice;
    public GameObject MessageSuccess;
    const string PACK_1 = "com.woodennuts_unscrew.pack1";
    const string PACK_2 = "com.woodennuts_unscrew.pack2";
    const string PACK_3 = "com.woodennuts_unscrew.pack3";
    const string PACK_4 = "com.woodennuts_unscrew.pack4";
    const string PACK_5 = "com.woodennuts_unscrew.pack5";
    const string PACK_6 = "com.woodennuts_unscrew.pack6";
    const string PACK_7 = "com.woodennuts_unscrew.pack7";
    const string PACK_8 = "com.woodennuts_unscrew.pack8";
    const string PACK_9 = "com.woodennuts_unscrew.pack9";//You will receive 5 assists in the game.

    IStoreController m_StoreController;
    private String ConsumableId;
    public Data data;
    public Payload payload;
    public PayloadData payloadData;
    int numberHint;

    //SETUP BUILDER

    void Start(){
        listItems.Add(new ItemIAP(PACK_1, "0"));
        listItems.Add(new ItemIAP(PACK_2, "0"));
        listItems.Add(new ItemIAP(PACK_3, "0"));
        listItems.Add(new ItemIAP(PACK_4, "0"));
        listItems.Add(new ItemIAP(PACK_5, "0"));
        listItems.Add(new ItemIAP(PACK_6, "0"));
        listItems.Add(new ItemIAP(PACK_7, "0"));
        listItems.Add(new ItemIAP(PACK_8, "0"));
        listItems.Add(new ItemIAP(PACK_9, "0"));
    }

    public void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
         foreach (var item in listItems){
             builder.AddProduct(item.id, ProductType.Consumable);
         }
        Debug.Log("SetupBuilder");
        UnityPurchasing.Initialize(this, builder);
    }

    /** UI BUTTON EVENTs for PURCHASE **/
    public void HandleInitiatePurchase(int posPack)
    {
        String productId = PACK_1;
      switch (posPack)
        {
            case 2:productId = PACK_2;
                break;
            case 3:  productId = PACK_3;
                break;
            case 4:  productId = PACK_4;
                break;
            case 5:  productId = PACK_5;
                break;
            case 6:  productId = PACK_6;
                break;
            case 7:  productId = PACK_7;
                break;
            case 8: productId = PACK_8;
                break;
            case 9: productId = PACK_9;
                break;
        }
      SetHandlePurchase(productId);
    }

    private void SetHandlePurchase(String pack)
    {
         Debug.Log("SetHandlePurchase" + pack);
        ConsumableId = pack;
        m_StoreController.InitiatePurchase(pack);
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
            var hint = 5;
            switch (product.definition.id)
            {
                case PACK_2: hint = 50;
                    break;
                case PACK_3:  hint = 80;
                    break;
                case PACK_4:  hint = 160;
                    break;
                case PACK_5:  hint = 250;
                    break;
                case PACK_6:  hint = 360;
                    break;
                case PACK_7:  hint = 450;
                    break;
                case PACK_8: hint =550 ;
                    break;
                case PACK_9: hint =680 ;
                    break;
            } 
            HomeSceneButtonController.instance.SaveGold(hint);
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Init purchase success!");
        m_StoreController = controller;
          foreach (var item in listItems){
            Product product = m_StoreController.products.WithID(item.id);
            if (product != null && product.metadata != null)
            {
            Debug.Log($"Product ID: {item.id} - Price: {product.metadata.localizedPriceString}");
            item.price = product.metadata.localizedPriceString;
         }
        }
        HomeSceneButtonController.instance.SetLayoutItemIAP(listItems);

    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
    
    [Serializable]
    public class SkuDetails
    {
        public string productId;
        public string type;
        public string title;
        public string name;
        public string iconUrl;
        public string description;
        public string price;
        public long price_amount_micros;
        public string price_currency_code;
        public string skuDetailsToken;
    }
    
    [Serializable]
    public class Payload
    {
        public string json;
        public string signature;
        public List<SkuDetails> skuDetails;
        public PayloadData payloadData;
    }
    
    [Serializable]
    public class PayloadData
    {
        public string orderId;
        public string packageName;
        public string productId;
        public long purchaseTime;
        public int purchaseState;
        public string purchaseToken;
        public int quantity;
        public bool acknowledged;
    }

    [Serializable]
    public class Data
    {
        public string Payload;
        public string Store;
        public string TransactionID;
    }
}
