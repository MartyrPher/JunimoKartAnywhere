using StardewValley;

namespace JunimoKartAnywhere.Framework
{
    /// <summary>Interface to make sure a given class has a response</summary>
    interface IResponse
    {
         void RecieveResponse(Farmer who, string answer);
    }
}
