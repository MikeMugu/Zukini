using BoDi;

namespace Zukini.Steps
{
    public class BaseSteps
    {
        /// <summary>
        /// Represents the Injected ObjectContainer.
        /// </summary>
        private readonly IObjectContainer _objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public BaseSteps(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        /// <summary>
        /// Returns the registered PropertyBucket used for remembering properties
        /// between steps.
        /// </summary>
        protected PropertyBucket PropertyBucket
        {
            get
            {
                return _objectContainer.Resolve<PropertyBucket>();
            }
        }

        /// <summary>
        /// Returns the ObjectContainer used for Dependency Injection
        /// </summary>
        protected IObjectContainer ObjectContainer
        {
            get
            {
                return _objectContainer;
            }
        }


        /// <summary>
        /// Returns the uniquely generated TestId associated with this test.
        /// This is just a handy property to get the testid without 
        /// </summary>
        protected string TestId
        {
            get
            {
                var propertyBucket = _objectContainer.Resolve<PropertyBucket>();
                return propertyBucket.TestId;
            }
        }
    }
}
