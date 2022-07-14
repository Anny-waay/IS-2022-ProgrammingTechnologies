package my.sum;

public class JavaSum {
    static
    {
        System.load("/Users/annakomova/Desktop/TechProg/lab-1/javaTask1/libsumJava.dylib");
    }

    native public static int sum(int a, int b);
    Для имплементации методов используем специальные обозначения. Так в Java мы нужент файл jni.h, из которого мы берём типы
    public static void main(String[] args){
        JavaSum test = new JavaSum();
        System.out.println(test.sum(1,4));
    }
}