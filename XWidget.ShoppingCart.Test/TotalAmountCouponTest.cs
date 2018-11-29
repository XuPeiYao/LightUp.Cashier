using XWidget.ShoppingCart.Coupons;
using XWidget.ShoppingCart.Test.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xunit;

namespace XWidget.ShoppingCart.Test {
    public class TotalAmountCouponTest {
        [Fact(DisplayName = "��1000���E��A���i���Шϥ�")]
        public void Over1000Off10_DenyReuse() {
            var userHasCoupons = new ICoupon[] {
                new TotalAmountCoupon<Guid>() {
                    Id = Guid.NewGuid(),
                    AllowReuse = false,
                    Count = 2,
                    Threshold = 1000,
                    DiscountPercent = 10,
                    Name = "��1000���E��"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 15,
                                Price = 100,
                                Name = "�åͯ�(12�]��)"
                            }
                        }
                    )
            };

            var answer = order.TotalAmount * 0.9m;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }

        [Fact(DisplayName = "��1000���E��A�������Шϥ�")]
        public void Over1000Off10_AllowReuse_Limit() {
            var userHasCoupons = new ICoupon[] {
                new TotalAmountCoupon<Guid>() {
                    Id = Guid.NewGuid(),
                    AllowReuse = true,
                    Count = 2,
                    Threshold = 1000,
                    DiscountPercent = 10,
                    Name = "��1000���E��"
                }
            };

            var productId = Guid.NewGuid();

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = productId,
                                Count = 10,
                                Price = 100,
                                Name = "�åͯ�(12�]��)"
                            },
                            new TestOrderItem() {
                                Id = productId,
                                Count = 5,
                                Price = 100,
                                Name = "�åͯ�(12�]��)"
                            }
                        }
                    )
            };

            var answer = order.TotalAmount * 0.9m * 0.9m;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }

        [Fact(DisplayName = "��1000���E��A�L�����Шϥ�")]
        public void Over1000Off10_AllowReuse_Unlimit() {
            var userHasCoupons = new ICoupon[] {
                new TotalAmountCoupon<Guid>() {
                    Id = Guid.NewGuid(),
                    AllowReuse = true,
                    Threshold = 1000,
                    DiscountPercent = 10,
                    Name = "��1000���E��"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 15,
                                Price = 100,
                                Name = "�åͯ�(12�]��)"
                            }
                        }
                    )
            };

            var answer = order.TotalAmount * 0.9m * 0.9m * 0.9m * 0.9m;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }
    }
}
