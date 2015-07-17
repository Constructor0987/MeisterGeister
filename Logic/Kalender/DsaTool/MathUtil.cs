using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender.DsaTool
{

/**
 * Simple mathematical routines.
 *
 * @author Copyright (c) 1998 Peter Diefenbach (peter@pdiefenbach.de)
 */
public class MathUtil {
	/**
	 * generates a (nearly) equally distributed integer random number
	 * in the range from 0 to m - 1 (if m is positive)
	 * or from m + 1 to 0 (if m is negative)
	 * @param m the upper bound for the random number (if m is positive)
	 */
	public static int random(int m) {
		if (0 == m) {
			// random(0) should behave nicely
			return 0;
		}
		if (m < 0) {
			return -random(-m);
		}
		// Get a random number, shift the sign away and do a modulo.
		return ((randomGenerator.Next() >> 1) % m);
	}

	/**
	 * generates a (nearly) equally distributed double random number
	 * in the range from 0 to m (including 0, excluding m) if m is positive,
	 * or from m to 0 (including 0, excluding m) if m is negative
	 * @param m the upper bound for the random number (if m is positive)
	 */
	public static double random(double m) {
		if (0.0 == m) {
			// random(0) should behave nicely
			return 0.0;
		}
		if (m < 0.0) {
			return -random(-m);
		}
		// Get a random number in the range [0,1) and multiply with m to get the range [0,m)
		return (randomGenerator.NextDouble() * m);
	}

	/**
	 * chooses an integer random number in the range from 0 to distribution.length-1.
	 * If distribution[0] is two times the value of distribution[1], then it's twice as likely that the result is 0 than 1.
	 * @exception java.lang.ArrayIndexOutOfBoundsException is thrown if all probabilities in <em>distribution</em> are 0.
	 * @exception java.lang.NullPointerException is thrown if <em>distribution</em> is <code>null</code>
	 * @return index in the range from 0 to distribution.length - 1
	 */
	public static int randomDistributed(int[] distribution) {
		int probabilitySum = 0;
		for (int i = 0; i < distribution.Length; i++) {
			if (0 < distribution[i]) {
				probabilitySum += distribution[i];
			}
		}
		int probabilityValue = random(probabilitySum);
		probabilitySum = 0;
		for (int index = 0; index < distribution.Length; index++) {
			if (0 < distribution[index]) {
				probabilitySum += distribution[index];
				if (probabilityValue < probabilitySum) {
					return index;
				}
			}
		}
		throw new IndexOutOfRangeException("All probabilities in the distribution were 0");
	}

	/**
	 * chooses an integer random number in the range from 0 to distribution.length-1.
	 * If distribution[0] is two times the value of distribution[1], then it's twice as likely that the result is 0 than 1.
	 * @exception java.lang.ArrayIndexOutOfBoundsException is thrown if all probabilities in <em>distribution</em> are 0
	 * @exception java.lang.NullPointerException is thrown if <em>distribution</em> is <code>null</code>
	 * @return index in the range from 0 to distribution.length - 1
	 */
	public static int randomDistributed(double[] distribution) {
		double probabilitySum = 0.0;
		for (int i = 0; i < distribution.Length; i++) {
			if (0.0 < distribution[i]) {
				probabilitySum += distribution[i];
			}
		}
		double probabilityValue = random(probabilitySum);
		probabilitySum = 0.0;
		for (int index = 0; index < distribution.Length; index++) {
			if (0.0 < distribution[index]) {
				probabilitySum += distribution[index];
				if (probabilityValue < probabilitySum) {
					return index;
				}
			}
		}
		throw new IndexOutOfRangeException("All probabilities in the distribution were 0");
	}

	/**
	 *	A friendlier modulo function, that behaves more like the counter model, or kitchen clock model:
	 *  If it's 1 o'clock and I turn back the clock about 1 hour, it's not zero o'clock, but 12 o'clock.
	 *  If I turn it back one more hour, it's not -1 o'clock, but 11 o'clock. And so on.
	 * <br></br>To give some more hints for the concepts: The following equation is always true.
	 * <pre>
	 *     modulo(val, x, y) = -modulo(-val, -x, -y)
	 *     modulo(val, x, y) = modulo(val - y, x, 0) + y
	 * </pre>
	 *  Examples:<br></br>
	 *  If offset is 0, the behavior is nearly like normal the modulo operator (%)
	 *  Otherwise it tells the lower bound of the destination range. Even more examples:<br></br>
	 *  <table border="1">
	 *  <tr><th> call                     </th><th>             result </th><th> comment </th></tr>
	 *  <tr><td><tt>modulo( 12, 5, 0)</tt></td><td align=\"right\">  2 </td><td> this behaves like the normal modulo operation (12 % 5)</td></tr>
	 *  <tr><td><tt>modulo(-12, 5, 0)</tt></td><td align=\"right\">  3 </td><td> To negative numbers, we add full circles (mod) till it's positive </td></tr>
	 *  <tr><td><tt>modulo( 12,-5, 0)</tt></td><td align=\"right\"> -3 </td><td> With negative bounds, the results are reverted (whoever that needs...) </td></tr>
	 *  <tr><td><tt>modulo(-12,-5, 0)</tt></td><td align=\"right\"> -2 </td><td> With negative bounds, the results are reverted (whoever that needs...) </td></tr>
	 *  <tr><td><tt>modulo(  1,12, 1)</tt></td><td align=\"right\">  1 </td><td> limits any number to the range 1..12, like the kitchen clock. In the range all stays the same. </td></tr>
	 *  <tr><td><tt>modulo( 12,12, 1)</tt></td><td align=\"right\"> 12 </td><td> limits any number to the range 1..12, like the kitchen clock. In the range all stays the same. </td></tr>
	 *  <tr><td><tt>modulo( 13,12, 1)</tt></td><td align=\"right\">  1 </td><td> limits any number to the range 1..12, like the kitchen clock. </td></tr>
	 *  <tr><td><tt>modulo( 14,12, 1)</tt></td><td align=\"right\">  2 </td><td> limits any number to the range 1..12, like the kitchen clock. </td></tr>
	 *  <tr><td><tt>modulo(  0,12, 1)</tt></td><td align=\"right\"> 12 </td><td> limits any number to the range 1..12, like the kitchen clock. </td></tr>
	 *  <tr><td><tt>modulo(  0, 3,-1)</tt></td><td align=\"right\">  0 </td><td> limits any number to the range -1..1. In the range all stays the same. </td></tr>
	 *  <tr><td><tt>modulo(  2, 3,-1)</tt></td><td align=\"right\"> -1 </td><td> limits any number to the range -1..1. </td></tr>
	 *  </table>
	 *
	 *  @param val The value to be modulated.
	 *  @param mod The upper bound (if offset is 0), or the length of the destination range (if offset is not 0).
	 *	@param offset The lower bound of the destination range.
	 */
	public static long modulo(long val, long mod, long offset) {
		// Filter out invalid values
		if (0 == mod) {
			// Behave nicely
			return 0;
		}
		// First do a normation and get rid of the offset
		val = val - offset;
		// Now have a look at all the different cases
		if (mod < 0) {
			return -modulo(-val, -mod, 0) + offset;
		}
		if (val < 0) {
			long a = (-val) % mod;
			if (0 == a) {
				return (a) + offset;
			}
			return (mod - a) + offset;
		}
		return (val % mod) + offset;
	}

	 /// <summary>
     /// A shortcut for modulo(<em>val</em>, <em>mod</em>, 0).
	 /// </summary>
	 /// <param name="val"></param>
	 /// <param name="mod"></param>
	 /// <returns></returns>
	public static long modulo(long val, long mod) {
		return modulo(val, mod, 0);
	}

	/**
	 * An adequate division method fitting for modulo.
	 * The intention is that for every non-zero x and every y the following equation is true:
	 * <pre>
	 *     val = x * divisio(val, x, y) + modulo(val, x, y)
	 * </pre>
	 * To understand that concept, start out with y=0 and x>0.
	 * <br></br>To give some more hints for the concepts: The following equations are always true.
	 * <pre>
	 *     divisio(val, x, y) = divisio(-val, -x, -y)
	 *     divisio(val, x, y) = divisio(val - y, x, 0)
	 * </pre>
	 *  Examples:<br></br>
	 *  If offset is 0, the behavior is nearly like normal the division operator (/)
	 *  Otherwise it tells the lower bound of the destination range. Even more examples:<br></br>
	 *  <table border>
	 *  <tr><th> call                      </th><th>             result </th><th> comment </th></tr>
	 *  <tr><td><tt>divisio(  0, 5, 0)</tt></td><td align=\"right\">  0 </td><td> this behaves like the normal division operation (0 / 5)</td></tr>
	 *  <tr><td><tt>divisio( 12, 5, 0)</tt></td><td align=\"right\">  2 </td><td> this behaves like the normal division operation (12 / 5)</td></tr>
	 *  <tr><td><tt>divisio( -1, 5, 0)</tt></td><td align=\"right\"> -1 </td><td> -1 is the last number of the smallest "negative" circle</td></tr>
	 *  <tr><td><tt>divisio( -5, 5, 0)</tt></td><td align=\"right\"> -1 </td><td> -5 is the first number of the smallest "negative" circle</td></tr>
	 *  <tr><td><tt>divisio( -6, 5, 0)</tt></td><td align=\"right\"> -2 </td><td> -6 is the last number of the second smallest "negative" circle</td></tr>
	 *  <tr><td><tt>divisio(  1,-5, 0)</tt></td><td align=\"right\"> -1 </td><td> With negative bounds, the results are reverted (whoever that needs...) </td></tr>
	 *  <tr><td><tt>divisio(  0,-5, 0)</tt></td><td align=\"right\">  0 </td><td> With negative bounds, the results are reverted (whoever that needs...) </td></tr>
	 *  </table>
	 *  @param val The value to be divided.
	 *  @param div The upper bound (if offset is 0), or the length of the destination range (if offset is not 0).
	 *	@param offset The lower bound of the destination range.
	 *  @throws ArithmeticException thrown if <em>div</em> is 0.
	 *  @see #modulo
	 */
    public static long divisio(long val, long div, long offset)
    {
		// First do a normation and get rid of the offset
		val = val - offset;
		// Now have a look at all the different cases
		if (div < 0) {
			return divisio(-val, -div, 0);
		}
		if (0 <= val) {
			// For positive val, it's a normal division
			return (val / div);
		} else {
			// For negative val, we have to take special care, as there is no "-0"
			return ((val + (1 - div)) / div);
		}
	}

	/**
	 *	A shortcut for divisio(<em>val</em>, <em>div</em>, 0).
	 *
	 * @see #divisio
	 */
    public static long divisio(long val, long div)
    {
		return divisio(val, div, 0);
	}

	/**
	 * An adequate division method fitting for modulo.
	 * The intention is that for every non-zero x and every y the following equation is true:
	 * <pre>
	 *     val = x * divisio(val, x, y) + modulo(val, x, y)
	 * </pre>
	 * To understand that concept, start out with y=0 and x>0.
	 * <br></br>To give some more hints for the concepts: The following equations are always true.
	 * <pre>
	 *     divisio(val, x, y) = divisio(-val, -x, -y)
	 *     divisio(val, x, y) = divisio(val - y, x, 0)
	 * </pre>
	 *  Examples:<br></br>
	 *  If offset is 0, the behavior is nearly like normal the division operator (/)
	 *  Otherwise it tells the lower bound of the destination range. Even more examples:<br></br>
	 *  <table border>
	 *  <tr><th> call                      </th><th>             result </th><th> comment </th></tr>
	 *  <tr><td><tt>divisio(  0, 5, 0)</tt></td><td align=\"right\">  0 </td><td> this behaves like the normal division operation (0 / 5)</td></tr>
	 *  <tr><td><tt>divisio( 12, 5, 0)</tt></td><td align=\"right\">  2 </td><td> this behaves like the normal division operation (12 / 5)</td></tr>
	 *  <tr><td><tt>divisio( -1, 5, 0)</tt></td><td align=\"right\"> -1 </td><td> -1 is the last number of the smallest "negative" circle</td></tr>
	 *  <tr><td><tt>divisio( -5, 5, 0)</tt></td><td align=\"right\"> -1 </td><td> -5 is the first number of the smallest "negative" circle</td></tr>
	 *  <tr><td><tt>divisio( -6, 5, 0)</tt></td><td align=\"right\"> -2 </td><td> -6 is the last number of the second smallest "negative" circle</td></tr>
	 *  <tr><td><tt>divisio(  1,-5, 0)</tt></td><td align=\"right\"> -1 </td><td> With negative bounds, the results are reverted (whoever that needs...) </td></tr>
	 *  <tr><td><tt>divisio(  0,-5, 0)</tt></td><td align=\"right\">  0 </td><td> With negative bounds, the results are reverted (whoever that needs...) </td></tr>
	 *  </table>
	 *  @param val The value to be divided.
	 *  @param div The upper bound (if offset is 0), or the length of the destination range (if offset is not 0).
	 *	@param offset The lower bound of the destination range.
	 *  @throws ArithmeticException thrown if <em>div</em> is 0.
	 *  @see #modulo(double, double, double)
	 */
	public static int divisio(double val, double div, double offset) {
		// First do a normation and get rid of the offset
		val = val - offset;
        double dres = val / div;
        if (Double.IsNaN(dres)) {
        	throw new ArithmeticException("Division by zero");
        }
        return (int)Math.Floor(dres);
	}

	/**
	 *	A shortcut for divisio(<em>val</em>, <em>div</em>, 0).
	 *
	 * @see #divisio(double, double, double)
	 */
	public static int divisio(double val, double div) {
		return divisio(val, div, 0);
	}

	/**
	 * The counterpart of my extended modulo, adapted for doubles.
	 * <br>Example: 11.0 mod 2.5 = (11.0 - 4 * 2.5) mod 2.5 = (11.0 - 10.0) mod 2.5 = 1.0
	 *
	 *  @param val The value to be modulated.
	 *  @param mod The upper bound (if offset is 0), or the length of the destination range (if offset is not 0).
	 *	@param offset The lower bound of the destination range.
	 */
	public static double modulo(double val, double mod, double offset) {
		// Filter out invalid values
		if (0.0 == mod) {
			// Behave nicely.
			return 0.0;
		}
        return val - mod * divisio(val, mod, offset);
	}

	/**
	 * A bounds function, cutting values exceeding the given bounds
	 * into the nearest bound.
	 * @param val The value to be bound.
	 * @param min The lower bound. If value is lesser than this bound, this bound is returned.
	 * @param max The upper bound. If value is greater than this bound, this bound is returned.
	 * @return <em>val</em> if it is in the range, otherwise <em>min</em> or <em>max</em>.
	 */
	public static int bound(int val, int min, int max) {
		if (val < min)	return min;
		if (val > max)	return max;
		return val;
	}

	/**
	 *	A bounds function, cutting values exceeding the given bounds
	 *	into the nearest bound.
	 * @param val The value to be bound.
	 * @param min The lower bound. If value is lesser than this bound, this bound is returned.
	 * @param max The upper bound. If value is greater than this bound, this bound is returned.
	 * @return <em>val</em> if it is in the range, otherwise <em>min</em> or <em>max</em>.
	 */
	public static double bound(double val, double min, double max) {
		if (val < min)	return min;
		if (val > max)	return max;
		return val;
	}

	/**
	 *	A signum function.
	 *  @param val The value the sign is to be checked.
	 *	@return -1, if <em>val</em> is negative, +1 if <em>val</em> is positive, and 0 if <em>val</em> is 0.
	 */
	public static int sgn(double val) {
		if (val < 0)	return -1;
		if (val > 0)	return  1;
		return 0;
	}

	/**
	 *	A signum function.
	 *  @param val The value the sign is to be checked.
	 *	@return -1, if <em>val</em> is negative, +1 if <em>val</em> is positive, and 0 if <em>val</em> is 0.
	 */
	public static int sgn(int val) {
		if (val < 0)	return -1;
		if (val > 0)	return  1;
		return 0;
	}

	private static Random randomGenerator = new Random();
}

}
