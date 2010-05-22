using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MongoDB
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class MongoRegex : IEquatable<MongoRegex>, IXmlSerializable
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "MongoRegex" /> class.
        /// </summary>
        public MongoRegex()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MongoRegex" /> class.
        /// </summary>
        /// <param name = "expression">The expression.</param>
        public MongoRegex(string expression)
            : this(expression, string.Empty)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MongoRegex" /> class.
        /// </summary>
        /// <param name = "expression">The expression.</param>
        /// <param name = "options">The options.</param>
        public MongoRegex(string expression, string options)
        {
            Expression = expression;
            Options = options;
        }

        /// <summary>
        ///   A valid regex string including the enclosing / characters.
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        ///   A string that may contain only the characters 'g', 'i', and 'm'. 
        ///   Because the JS and TenGen representations support a limited range of options, 
        ///   any nonconforming options will be dropped when converting to this representation
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        ///   Determines whether the specified <see cref = "System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name = "obj">The <see cref = "System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref = "System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref = "T:System.NullReferenceException">
        ///   The <paramref name = "obj" /> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj))
                return false;
            if(ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == typeof(MongoRegex) && Equals((MongoRegex)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(MongoRegex other)
        {
            if(ReferenceEquals(null, other))
                return false;
            if(ReferenceEquals(this, other))
                return true;
            return Equals(other.Expression, Expression) && Equals(other.Options, Options);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(MongoRegex left, MongoRegex right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(MongoRegex left, MongoRegex right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///   A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Expression != null ? Expression.GetHashCode() : 0)*397) ^ (Options != null ? Options.GetHashCode() : 0);
            }
        }

        /// <summary>
        ///   Returns a <see cref = "System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///   A <see cref = "System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}{1}", Expression, Options);
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if(reader.MoveToAttribute("options"))
                Options = reader.Value;

            if(reader.IsEmptyElement)
                return;

            Expression = reader.ReadString();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if(Options!=null)
                writer.WriteAttributeString("options", Options);

            if(Expression==null)
                return;

            writer.WriteString(Expression);
        }
    }
}