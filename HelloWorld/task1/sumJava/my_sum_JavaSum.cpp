#include <iostream>
#include "my_sum_JavaSum.h"

JNIEXPORT jint JNICALL Java_my_sum_JavaSum_sum
        (JNIEnv * env, jobject obj, jint a, jint b){
    int result = (int)(a+b);
    return (jint)result;
}
