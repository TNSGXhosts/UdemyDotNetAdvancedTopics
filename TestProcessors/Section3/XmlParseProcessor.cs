using System.Dynamic;
using System.Xml.Linq;

namespace TestProcessors;

public class XmlParseProcessor : ITestProcessor
{
    public void Execute()
    {
        Console.WriteLine("XML parsing using dynamic demonstration:");

        var xml = @"<people>
                        <person name='Vit'/>
                    </people>";

        var node = XElement.Parse(xml);
        dynamic dnode = new DynamicXmlNode(node);
        Console.WriteLine(dnode.person.name); 
    }
}

public class DynamicXmlNode : DynamicObject
{
    private readonly XElement _node;

    public DynamicXmlNode(XElement node)
    {
        _node = node;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        var el = _node.Element(binder.Name);

        if (el != null) 
        {
            result = new DynamicXmlNode(el);
            return true;
        } else {
            var attribute = _node.Attribute(binder.Name);
            
            if (attribute != null)
            {
                result = attribute.Value;
                return true;
            } else {
                result = null;
                return false;
            }
        }
    }
}