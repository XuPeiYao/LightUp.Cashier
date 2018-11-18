using LightUp.Cashier.Coupon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace LightUp.Cashier.Test {
    public class UnitTest1 {
        [Fact(DisplayName = "��1000�E��")]
        public void Test1() {
            var cashier = new DefaultCashier<int>();
            // �ĤG��6��
            cashier.CouponList.Add(new SecondPieceDiscount<int>() {
                OffPercent = 40,
                Items = new List<int>() { 1 }
            });

            // ��1000��9��
            cashier.CouponList.Add(new Discount<int>() {
                OffPercent = 10,
                Threshold = 1000
            });

            var order = new Order<int>(new OrderItem<int>[]  {
                new OrderItem<int>() {
                    Id = 0,
                    UnitPrice = 150,
                    Count = 17,
                    Name = "�åͯ�"
                },
                new OrderItem<int>() {
                    Id = 1,
                    UnitPrice = 100,
                    Count = 5,
                    Name = "���O��"
                }
            });
            cashier.Checkout(order);
            string data = "";
            foreach (var item in order.Items) {
                data += "\r\n" + ($"{item.Name}\t���:{item.UnitPrice}\t�ƶq:{item.Count}\t���B:{item.TotalPrice}");
            }

            Assert.Equal((150m * 17m + (100 + 60) * 2 + 100) * 0.9m, order.Amount);
        }
    }
}
