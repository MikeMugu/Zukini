using BoDi;

namespace Zukini.UI.Steps
{
    /// <summary>
    /// The context class allows users to specify their own data for their step definition classes
    /// The Zukini StepsContext provides the property bucket and test id by default for the ApiSteps and UISteps
    /// </summary>
    /// <remarks>
    ///     There are two copies of ZukiniStepContext, one in Zukini.API.Steps and one in Zukini.UI.Steps
    ///     this is because there is a bug in .NET Framework versions 4.5.(0-2) - 4.6.1 that causes a compile error when
    ///     other projects attempts to inherit from ApiSteps or UISteps
    ///     and when ZukiniStepContext is defined in Zukini.Steps
    ///     Error CS0012  The type 'ZukiniStepContext' is defined in an assembly that is not referenced.You must add a reference to assembly 'Zukini, Version=1.2.5.0, Culture=neutral, PublicKeyToken=null'.	UnitTestProject1 C:\QA\Zukini\UnitTestProject1\UnitTest1.cs  8	Active
    /// </remarks>
    public class ZukiniStepContext
    {
        /// <summary>
        /// Represents the Injected ObjectContainer.
        /// </summary>
        private readonly IObjectContainer _objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public ZukiniStepContext(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
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
        /// Returns the registered PropertyBucket used for remembering properties
        /// between steps.
        /// </summary>
        public PropertyBucket PropertyBucket
        {
            get
            {
                return _objectContainer.Resolve<PropertyBucket>();
            }
        }

        /// <summary>
        /// Returns the uniquely generated TestId associated with this test.
        /// This is just a handy property to get the testid without 
        /// </summary>
        public string TestId
        {
            get
            {
                var propertyBucket = _objectContainer.Resolve<PropertyBucket>();
                return propertyBucket.TestId;
            }
        }
    }
}
