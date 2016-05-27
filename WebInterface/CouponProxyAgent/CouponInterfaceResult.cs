using System;
using System.Collections.Generic;
using System.Text;

namespace WebInterface
{
    /// <summary>
    /// 使用优惠券接口返回结果实体类
    /// </summary>
    public class UseCouponResult
    {
        private int _code;
        /// <summary>
        /// 返回状态码
        /// </summary>
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _message;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        private UseResult _data;
        /// <summary>
        /// 返回的数据内容
        /// </summary>
        public UseResult data
        {
            get { return _data; }
            set { _data = value; }
        }

    }

    /// <summary>
    /// 获取车场可用优惠券接口的返回结果
    /// </summary>
    public class GetParkCouponsResult
    {
        private int _code;
        /// <summary>
        /// 返回状态码
        /// </summary>
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _message;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        private List<DataValue> _data;
        /// <summary>
        /// 优惠券数据
        /// </summary>
        public List<DataValue> data
        {
            get { return _data; }
            set { _data = value; }
        }

    }

    /// <summary>
    /// 获取获取用户可用优惠券接口的返回结果
    /// </summary>
    public class GetAvailableResult
    {
        private int _code;
        /// <summary>
        /// 返回状态码
        /// </summary>
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _message;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        private AvailableValue _data;
        /// <summary>
        /// 优惠券数据
        /// </summary>
        public AvailableValue data
        {
            get { return _data; }
            set { _data = value; }
        }

    }

    /// <summary>
    /// 接口返回的数据实体类
    /// </summary>
    public class DataValue
    {
        private int _id;
        /// <summary>
        /// 优惠券ID
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _parkId;
        /// <summary>
        /// 停车场ID
        /// </summary>
        public string parkId
        {
            get { return _parkId; }
            set { _parkId = value; }
        }

        private string _businessId;
        /// <summary>
        /// 商户ID
        /// </summary>
        public string businessId
        {
            get { return _businessId; }
            set { _businessId = value; }
        }

        private string _businessName;
        /// <summary>
        /// 商户名称
        /// </summary>
        public string businessName
        {
            get { return _businessName; }
            set { _businessName = value; }
        }

        private string _businessLogo;
        /// <summary>
        /// 商家LOGO图片地址
        /// </summary>
        public string businessLogo
        {
            get { return _businessLogo; }
            set { _businessLogo = value; }
        }

        private string _name;
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _price;
        /// <summary>
        /// 商家和车场结算金额（C端用不上）
        /// </summary>
        public string price
        {
            get { return _price; }
            set { _price = value; }
        }

        private string _category;
        /// <summary>
        /// 减免类型
        /// 1 小时优惠券 多少小时
        /// 2 金额优惠券 多少元
        /// 3 折扣优惠券 几折
        /// 4 免费券
        /// </summary>
        public string category
        {
            get { return _category; }
            set { _category = value; }
        }

        private string _categoryValue;
        /// <summary>
        /// 减免值
        /// </summary>
        public string categoryValue
        {
            get { return _categoryValue; }
            set { _categoryValue = value; }
        }

        private int _amount;
        /// <summary>
        /// 优免的数量
        /// </summary>
        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private string _createTime;
        /// <summary>
        /// 优免创建时间
        /// </summary>
        public string createTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        private int _usedTotal;
        /// <summary>
        /// 累计使用次数
        /// </summary>
        public int usedTotal
        {
            get { return _usedTotal; }
            set { _usedTotal = value; }
        }

        private string _status;
        /// <summary>
        /// 优免的状态，0:停用，1：启用，9:删除
        /// </summary>
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _remark;
        /// <summary>
        /// 优免描述
        /// </summary>
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private string _couponType;
        /// <summary>
        /// 优免券类型：0:普通券 1:二维码券
        /// </summary>
        public string couponType
        {
            get { return _couponType; }
            set { _couponType = value; }
        }

        private string _effectiveStart;
        /// <summary>
        /// 优免券有效时间-开始时间
        /// </summary>
        public string effectiveStart
        {
            get { return _effectiveStart; }
            set { _effectiveStart = value; }
        }

        private string _effectiveEnd;
        /// <summary>
        /// 优免券有效时间-结束时间
        /// </summary>
        public string effectiveEnd
        {
            get { return _effectiveEnd; }
            set { _effectiveEnd = value; }
        }
    }

    public class AvailableValue
    {
        private int _id;
        /// <summary>
        /// ID
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _couponId;
        /// <summary>
        /// 优惠券ID
        /// </summary>
        public int couponId
        {
            get { return _couponId; }
            set { _couponId = value; }
        }

        private string _couponFreeName;
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string couponFreeName
        {
            get { return _couponFreeName; }
            set { _couponFreeName = value; }
        }

        private string _category;
        /// <summary>
        /// 减免类型
        /// 1 小时优惠券 多少小时
        /// 2 金额优惠券 多少元
        /// 3 折扣优惠券 几折
        /// 4 免费券
        /// </summary>
        public string category
        {
            get { return _category; }
            set { _category = value; }
        }

        private string _categoryValue;
        /// <summary>
        /// 减免值
        /// </summary>
        public string categoryValue
        {
            get { return _categoryValue; }
            set { _categoryValue = value; }
        }

        private string _vehicleNo;
        /// <summary>
        /// 车牌号
        /// </summary>
        public string vehicleNo
        {
            get { return _vehicleNo; }
            set { _vehicleNo = value; }
        }

    }

    /// <summary>
    /// 使用优惠券的返回数据
    /// </summary>
    public class UseResult
    {
        private int _couponFreeI;
        /// <summary>
        /// 优惠券ID
        /// </summary>
        public int couponFreeId
        {
            get { return _couponFreeI; }
            set { _couponFreeI = value; }
        }

        private string _parking_record_id;
        /// <summary>
        /// 停车记录GUID
        /// </summary>
        public string parking_record_id
        {
            get { return _parking_record_id; }
            set { _parking_record_id = value; }
        }
    }

}
