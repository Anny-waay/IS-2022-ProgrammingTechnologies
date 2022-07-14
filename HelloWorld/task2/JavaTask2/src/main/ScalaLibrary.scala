package main

import scala.math.tan
import scala.util.chaining.scalaUtilChainingOps

object ScalaLibrary {

  def double(x: Int) = x * 2
  def square(x: Int): Int = x * x
  def double_square(x: Int): Int = x.pipe(double).pipe(square)

  sealed trait Polygon
  case class RegularTriangle(c1: Int, c2: Int) extends Polygon
  case class Rectangle(h: Int, w: Int) extends Polygon
  case class RegularPolygon(side: Int, n: Int) extends Polygon

  def area (polygon: Polygon): Double = polygon match {
    case RegularTriangle(c1, c2) => 0.5 * c1 * c2
    case Rectangle(h, w) => h * w
    case RegularPolygon(side, n) => n * square(side) / ( 4 * tan(180/n))
  }

  def main(args: Array[String]): Unit = {
  }
}
