using BUnit.Test.ClassHelpers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;

namespace BUnit.Test
{
    public class SetupParameterListTest
    {
        /// <summary>
        /// Тестируем передачу в выражение ссылку на переменную типа Const
        /// </summary>
        [Fact]
        public void WriteValueTypeConstant()
        {
            //arrange
            var expressionHelper = new ExpressionHelper<SetupExpressionHelper>();
            var expression = expressionHelper.Setup(x => x.Write(5));

            //act
            var setupParametrList = expression.Arguments.ToSetupParameterList();

            //asser
            Assert.True(setupParametrList.Count == 1);
            Assert.True(setupParametrList[0].Type == typeof(int));
            Assert.True(setupParametrList[0].SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(Convert.ToInt32(setupParametrList[0].Value) == 5);
        }

        /// <summary>
        /// Тестируем передачу в выражение ссылку на переменную типа ValuType
        /// </summary>
        /// <remarks>
        /// Также здесь демонстрируется пример замыкания.
        /// </remarks>
        [Fact]
        public void WriteValueTypeMemeber()
        {
            //arrange
            var cn = 5;
            var expressionHelper = new ExpressionHelper<SetupExpressionHelper>();
            var expression = expressionHelper.Setup(x => x.Write(cn));

            //act
            cn = 6;
            var setupParametrList = expression.Arguments.ToSetupParameterList();

            //asser
            Assert.True(setupParametrList.Count == 1);
            Assert.True(setupParametrList[0].Type == typeof(int));
            Assert.True(setupParametrList[0].SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True(Convert.ToInt32(setupParametrList[0].Value) == cn);
        }


        /// <summary>
        /// Тестируем передачу в выражение ссылку на ссылочную переменную
        /// созданную прямо в выражении
        /// </summary>
        [Fact]
        public void WriteNewReferenceType()
        {
            //arrange
            var expressionHelper = new ExpressionHelper<SetupExpressionHelper>();
            var expression = expressionHelper.Setup(x => x.Write(new StringBuilder()));

            //act
            var setupParametrList = expression.Arguments.ToSetupParameterList();

            //asser
            Assert.True(setupParametrList.Count == 1);
            Assert.True(setupParametrList[0].Type == typeof(StringBuilder));
            Assert.True(setupParametrList[0].SetupArgumentType == SetupArgumentType.Ref);
        }

        /// <summary>
        /// Тестируем передачу в выражение ссылку на ссылочную переменную
        /// </summary>
        [Fact]
        public void WriteCallReferenceType()
        {
            //arrange
            var sb = new StringBuilder();
            var expressionHelper = new ExpressionHelper<SetupExpressionHelper>();
            var expression = expressionHelper.Setup(x => x.Write(sb));

            //act
            var setupParametrList = expression.Arguments.ToSetupParameterList();

            //assert
            Assert.True(setupParametrList.Count == 1);
            Assert.True(setupParametrList[0].Type == typeof(StringBuilder));
            Assert.True(setupParametrList[0].SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True(sb.Equals(setupParametrList[0].Value));
        }


        [Fact]
        public void WriteEnumTypeConstant()
        {
            //arrange
            var expressionHelper = new ExpressionHelper<SetupExpressionHelper>();
            var expression = expressionHelper.Setup(x => x.Write(SetupExpressionHelperEmum.Four));

            //act
            var setupParametrList = expression.Arguments.ToSetupParameterList();

            //asser
            Assert.True(setupParametrList.Count == 1);
            Assert.True(setupParametrList[0].Type == typeof(SetupExpressionHelperEmum));
            Assert.True(setupParametrList[0].SetupArgumentType == SetupArgumentType.Constant);
            Assert.True((SetupExpressionHelperEmum)setupParametrList[0].Value == SetupExpressionHelperEmum.Four);
        }

        [Fact]
        public void WriteEnumTypeMember()
        {
            //arrange
            var enumValue = SetupExpressionHelperEmum.Three;
            var expressionHelper = new ExpressionHelper<SetupExpressionHelper>();
            var expression = expressionHelper.Setup(x => x.Write(enumValue));

            //act
            var setupParametrList = expression.Arguments.ToSetupParameterList();

            //asser
            Assert.True(setupParametrList.Count == 1);
            Assert.True(setupParametrList[0].Type == typeof(SetupExpressionHelperEmum));
            Assert.True(setupParametrList[0].SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True((SetupExpressionHelperEmum)setupParametrList[0].Value == SetupExpressionHelperEmum.Three);
        }
    }
}
