//这个脚本用来存放枚举类型
//时间2019-03-08

//模式
public enum Mode
{
    Default,//默认
    BrowseMode,//浏览模式
    StudyMode,//学习模式
    ExamMode//考试模式
}

//模块
public enum Moudule
{
    Default,//默认
    Moudule1,//模块1
    Moudule2,//模块2
    Moudule3,//模块3
    Moudule4//模块4
}

//组装块零件的种类
public enum AssembleCubePart
{
    Part1,
    Part2,
    Part3,
    Part4,
    Part5,
    Part6,
    Part7,
    Part8,
    Part9,
    Part10
}

public class EnumModel
{
    //枚举类单例
    private static EnumModel _instance = null;
    private EnumModel()
    {
    }
    public static EnumModel GetInstance()
    {
        if (_instance == null)
        {
            _instance = new EnumModel();
        }
        return _instance;
    }

    //模式
    public Mode m_Mode { get; set; }
    //模块
    public Moudule m_Moudule { get; set; }
}
