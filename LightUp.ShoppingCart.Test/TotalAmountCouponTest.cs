using LightUp.ShoppingCart.Coupons;
using LightUp.ShoppingCart.Test.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xunit;

namespace LightUp.ShoppingCart.Test {
    
    public class TotalAmountCouponTest {
        [Fact(DisplayName = "滿1000元九折，不可重覆使用")]
        public void Over1000Off10_DenyReuse() {
            var userHasCoupons = new ICoupon[] {
                new TotalAmountCoupon<Guid>() {
                    Id = Guid.NewGuid(),
                    AllowReuse = false,
                    Count = 2,
                    Threshold = 1000,
                    DiscountPercent = 10,
                    Name = "滿1000元九折"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 15,
                                Price = 100,
                                Name = "衛生紙(12包裝)"
                            }
                        }
                    )
            };

            var answer = order.TotalAmount * 0.9m;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }

        [Fact(DisplayName = "滿1000元九折，可重覆使用")]
        public void Over1000Off10_AllowReuse() {
            var userHasCoupons = new ICoupon[] {
                new TotalAmountCoupon<Guid>() {
                    Id = Guid.NewGuid(),
                    AllowReuse = true,
                    Count = 2,
                    Threshold = 1000,
                    DiscountPercent = 10,
                    Name = "滿1000元九折"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 15,
                                Price = 100,
                                Name = "衛生紙(12包裝)"
                            }
                        }
                    )
            };

            var answer = order.TotalAmount * 0.9m * 0.9m;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }
    }
}
