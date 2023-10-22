using System.Text.Json.Serialization;

namespace AB12.Application.Base.Common;

public abstract class SharedEntity
{
    protected SharedEntity()
    {
        ID = Guid.NewGuid().ToString().Replace("-", "");
    }

    public string ID { get; set; }
}
