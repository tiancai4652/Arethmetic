using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arethmetic.AsyncTask
{
    public class MyThread
    {

        #region 手动创建线程(不推荐)

        //因为在大量需要创建线程情况下，用Thread.Start去创建线程是会浪费线程资源，因为线程用完就没了，不具备重复利用能力
        //除非真的需要操作线程优先级
        //除非需要创建一个前台线程，由于类似于控制台程序当初始前台线程执行完就会退出进程，那么创建前台线程可以保证进程退出前该前台线程正常执行成功
        //除非需要创建一个后台线程，长时间执行的，（Task的TaskCreationOptions.LongRunning内部也是创建了一个后台线程Thread，而不是在ThreadPool执行）



        public void ThreadCreatStart()
        {
            Thread thread = new Thread(MethodNoParams);
            thread.IsBackground = true;//设置为后台线程，默认前台线程
         
        }

        public void ThreadCreat2(object obj)
        {
            Thread thread = new Thread((x)=>MethodWithParam(x));
            thread.Start(obj);
            thread.IsBackground = true;//设置为后台线程，默认前台线程
        }


        void MethodNoParams()
        { }

        void MethodWithParam(object obj)
        { }


        #region Join

        void MyThreadJoin()
        {
            //Thread.Join() 在MSDN中的解释：Blocks the calling thread until a thread terminates
            //当NewThread调用Join方法的时候，MainThread就被停止执行，
            //直到NewThread线程执行完毕。
        }



        #endregion



        #endregion

        #region ThreadPool

        //CLR在进程初始化时，CLR会开辟一块内存空间给ThreadPool
        //如果没有可用线程,线程池则会通过开辟工作线程(都是后台线程)去请求该队列执行任务
        //任务执行完毕则回返回线程池，线程池尽可能会用返回的工作线程去执行(减少开辟)

        public void ThreadPoolInit()
        {
            //优点:
            //默认线程池已经根据自身CPU情况做了配置，在需要复杂多任务并行时，智能在时间和空间上做到均衡，
            //在CPU密集型操作有一定优势，而不是像Thread.Start那样，需要自己去判断和考虑
            //缺点:
            //ThreadPool原生不支持对工作线程取消、完成、失败通知等交互性操作，同样不支持获取函数返回值，灵活度不够，Thread原生有Abort (同样不推荐)、Join等可选择
            //不适合LongTask，因为这类会造成线程池多创建线程(上述代码可知道)，这时候可以单独去用Thread去执行LongTask


            //微软不建议程序员使用这两个方法的原因是可能会影响到线程池中的性能
            //ThreadPool.SetMaxThreads(10,10);

            ThreadPool.GetMaxThreads(out int maxWorkThreadCount,
                                      out int maxIOThreadCount);

            ThreadPool.GetMinThreads(out int minWorkThreadCount,
                                   out int minIOThreadCount);


            //public static Boolean QueueUserWorkItem(WaitCallback wc, Object state);
            //State: 这个参数也是非常重要的，当执行带有参数的回调函数时，该参数会将引用传入，回调方法中，供其使用

            //执行20次任务
            for (int i = 0; i < 20; i++)
            {
                ThreadPool.QueueUserWorkItem(s =>
                {
                    var workThreadId = Thread.CurrentThread.ManagedThreadId;
                    var isBackground = Thread.CurrentThread.IsBackground;
                    var isThreadPool = Thread.CurrentThread.IsThreadPoolThread;
                    Console.WriteLine($"work is on thread {workThreadId}, " +
                        $"Now time:{ DateTime.Now.ToString("ss.ff")}," +
                            $" isBackground:{isBackground}, isThreadPool:{isThreadPool}");
                    Thread.Sleep(5000);//模拟工作线程运行
                });
            }

        }

        #endregion

        #region Task






        #endregion


    }
}
