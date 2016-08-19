// Version 1.4.2
using UnityEngine;

public class ClassExtensions {

    public static string[] stringsFrom00To99 ={"00","01","02","03","04","05","06","07","08","09",
                                                "10","11","12","13","14","15","16","17","18","19",
                                                "20","21","22","23","24","25","26","27","28","29",
                                                "30","31","32","33","34","35","36","37","38","39",
                                                "40","41","42","43","44","45","46","47","48","49",
                                                "50","51","52","53","54","55","56","57","58","59",
                                                "60","61","62","63","64","65","66","67","68","69",
                                                "70","71","72","73","74","75","76","77","78","79",
                                                "80","81","82","83","84","85","86","87","88","89",
                                                "90","91","92","93","94","95","96","97","98","99"};

    //-------------------------------[Mathfx]

    /// <summary>
    /// Hermite - This method will interpolate while easing in and out at the limits.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Hermite(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
    }

    /// <summary>
    /// Sinerp - Short for 'sinusoidal interpolation', this method will interpolate while easing around the end, when value is near one.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Sinerp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
    }

    /// <summary>
    /// Coserp - Similar to Sinerp, except it eases in, when value is near zero, instead of easing out (and uses cosine instead of sine).
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Coserp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, 1.0f - Mathf.Cos(value * Mathf.PI * 0.5f));
    }

    /// <summary>
    /// Berp - Short for 'boing-like interpolation', this method will first overshoot, then waver back and forth around the end value before coming to a rest.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Berp(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }

    /// <summary>
    /// SmoothStep - Works like Lerp, but has ease-in and ease-out of the values.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float SmoothStep(float x, float min, float max)
    {
        x = Mathf.Clamp(x, min, max);
        float v1 = (x - min) / (max - min);
        float v2 = (x - min) / (max - min);
        return -2 * v1 * v1 * v1 + 3 * v2 * v2;
    }

    /// <summary>
    /// Lerp - Short for 'linearly interpolate', this method is equivalent to Unity's Mathf.Lerp, included for comparison.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Lerp(float start, float end, float value)
    {
        return ((1.0f - value) * start) + (value * end);
    }

    /// <summary>
    /// NearestPoint - Will return the nearest point on a line to a point. Useful for making an object follow a track.
    /// </summary>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
    {
        Vector3 lineDirection = Vector3.Normalize(lineEnd - lineStart);
        float closestPoint = Vector3.Dot((point - lineStart), lineDirection) / Vector3.Dot(lineDirection, lineDirection);
        return lineStart + (closestPoint * lineDirection);
    }

    /// <summary>
    /// NearestPointStrict - Works like NearestPoint except the end of the line is clamped.
    /// </summary>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
    {
        Vector3 fullDirection = lineEnd - lineStart;
        Vector3 lineDirection = Vector3.Normalize(fullDirection);
        float closestPoint = Vector3.Dot((point - lineStart), lineDirection) / Vector3.Dot(lineDirection, lineDirection);
        return lineStart + (Mathf.Clamp(closestPoint, 0.0f, Vector3.Magnitude(fullDirection)) * lineDirection);
    }


    /// <summary>
    /// Bounce - Returns a value between 0 and 1 that can be used to easily make bouncing GUI items (a la OS X's Dock)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Bounce(float x)
    {
        return Mathf.Abs(Mathf.Sin(6.28f * (x + 1f) * (x + 1f)) * (1f - x));
    }
    
    /// <summary>
    /// test for value that is near specified float (due to floating point inprecision)
    /// all thanks to Opless for this!
    /// </summary>
    /// <param name="val"></param>
    /// <param name="about"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static bool Approx(float val, float about, float range)
    {
        return ((Mathf.Abs(val - about) < range));
    }

    /// <summary>
    /// test if a Vector3 is close to another Vector3 (due to floating point inprecision)
    /// compares the square of the distance to the square of the range as this
    /// avoids calculating a square root which is much slower than squaring the range
    /// </summary>
    /// <param name="val"></param>
    /// <param name="about"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static bool Approx(Vector3 val, Vector3 about, float range)
    {
        return ((val - about).sqrMagnitude < range * range);
    }

    /// <summary>
    /// * CLerp - Circular Lerp - is like lerp but handles the wraparound from 0 to 360.
    /// * This is useful when interpolating eulerAngles and the object
    /// * crosses the 0/360 boundary.  The standard Lerp function causes the object
    /// * to rotate in the wrong direction and looks stupid. Clerp fixes that.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Clerp(float start, float end, float value)
    {
        float min = 0.0f;
        float max = 360.0f;
        float half = Mathf.Abs((max - min) / 2.0f);//half the distance between min and max
        float retval = 0.0f;
        float diff = 0.0f;

        if ((end - start) < -half)
        {
            diff = ((max - start) + end) * value;
            retval = start + diff;
        }
        else if ((end - start) > half)
        {
            diff = -((max - end) + start) * value;
            retval = start + diff;
        }
        else retval = start + (end - start) * value;

        // Debug.Log("Start: "  + start + "   End: " + end + "  Value: " + value + "  Half: " + half + "  Diff: " + diff + "  Retval: " + retval);
        return retval;
    }

    //-------------------------------
    public static int GetBoundsVolume(Bounds rBounds)
    {
        return Mathf.CeilToInt(rBounds.size.sqrMagnitude);
    }


    /// <summary>
    /// Returns the distance between two points
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static float GetDistanceFast(Vector3 v1, Vector3 v2) {
        return (v1 - v2).magnitude;
    }

    /// <summary>
    /// Returns the distance between two points
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static float GetDistanceFast(Vector2 v1, Vector2 v2) {
        return (v1 - v2).magnitude;
    }

    /// <summary>
    /// Returns the distance between two points
    /// </summary>
    /// <param name="f1"></param>
    /// <param name="f2"></param>
    /// <returns></returns>
    public static float GetDistanceFast(float f1, float f2) {
        return Mathf.Abs(f2 - f1);
    }

    /// <summary>
    /// Returns whether an Bound is visible from the camera FrustumPlanes. It is more accurate than the viewport of the camera, but more expensive
    /// </summary>
    /// <param name="rBounds"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static bool BoundsIsVisibleFromPlanes(Bounds rBounds, Camera camera)
    {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, rBounds);
    }

    public static Vector3 BoundsClosestPoint(Bounds rBounds, Vector3 point)
    {
        Vector3 lineDirection = Vector3.Normalize(rBounds.max - rBounds.min);
        float closestPoint = Vector3.Dot((point - rBounds.min), lineDirection) / Vector3.Dot(lineDirection, lineDirection);
        return rBounds.min + (closestPoint * lineDirection);
    }

    /// <summary>
    /// Returns whether an Bound is visible from the camera viewPort
    /// </summary>
    /// <param name="rBounds"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static bool BoundsIsVisibleFromViewport(Bounds rBounds, Camera camera)
    {
        Vector3 viewPos;
#if (UNITY_4_6 || UNITY_4_5 || UNITY_4_4 || UNITY_4_3 || UNITY_4_2 || UNITY_4_1 || UNITY_4_0)
        viewPos = camera.WorldToViewportPoint(BoundsClosestPoint(rBounds, camera.transform.position));
#else
        viewPos = camera.WorldToViewportPoint(rBounds.ClosestPoint(camera.transform.position));
#endif

        if (viewPos.x < 1.1 && viewPos.x > -0.1 && viewPos.y < 1.1 && viewPos.y > -0.1 && viewPos.z > 0.5)
                return true;
            else return false;
    }


    /// <summary>
    /// Returns whether an GameObject is visible from the camera viewPort
    /// </summary>
    /// <param name="rBounds"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public bool GOIsVisibleFromViewport(GameObject rBounds, Camera camera)
    {
        Vector3 viewPos;

        viewPos = camera.WorldToViewportPoint(rBounds.transform.position);

        if (viewPos.x < 1.1 && viewPos.x > -0.1 && viewPos.y < 1.1 && viewPos.y > -0.1 && viewPos.z > 0.5)
            return true;
        else return false;
    }


    /// <summary>
    /// Returns whether a ray intersects the Bounds.
    /// </summary>
    /// <param name="rBounds"></param>
    /// <param name="origen"></param>
    /// <param name="direccion"></param>
    /// <returns></returns>
    public static bool BoundsIntersectVector(Bounds rBounds, Vector3 origen, Vector3 direccion)
    {
        Ray ray = new Ray(origen, direccion);
        return rBounds.IntersectRay(ray);
    }


    /// <summary>
    /// Returns true or false if the Bounds are obstructed by some collider with respect to the camera
    /// </summary>
    /// <param name="rBounds"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static bool BoundsIsHiddenBehind(Bounds rBounds, Camera camera)
    {
        RaycastHit hit;
#if (UNITY_4_6 || UNITY_4_5 || UNITY_4_4 || UNITY_4_3 || UNITY_4_2 || UNITY_4_1 || UNITY_4_0)
        if (Physics.Linecast(camera.transform.position, BoundsClosestPoint(rBounds,camera.transform.position), out hit) &&
            GetBoundsVolume(rBounds) < GetBoundsVolume(hit.collider.bounds))
            return true;
#else
        if (Physics.Linecast(camera.transform.position, rBounds.ClosestPoint(camera.transform.position), out hit) &&
            GetBoundsVolume(rBounds) < GetBoundsVolume(hit.collider.bounds))
            return true;
#endif

        else return false;
    }
    //-------------------------[Vector3 Extensions]

    /// <summary>
    /// Detects whether a point is visible within the screen in the camera
    /// </summary>
    /// <param name="point"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static bool Vector3IsVisibleFrom(Vector3 point, Camera camera)
    {
        Vector3 viewPos = camera.WorldToViewportPoint(point);
        if (viewPos.x < 1 && viewPos.x > 0 && viewPos.y < 1 && viewPos.y > 0 && viewPos.z > 0)
            return true;
        else return false;
    }

    /// <summary>
    /// It detects whether a point is behind another object from the camera
    /// </summary>
    /// <param name="point"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static bool Vector3IsHiddenBehind(Vector3 point, Camera camera)
    {
        RaycastHit hit;
        if (Physics.Linecast(camera.transform.position, point, out hit)) return true;
        else return false;
    }

    //-------------------------[Floats Extension]
    //https://en.wikipedia.org/wiki/Halton_sequence
    //https://en.wikipedia.org/wiki/Low-discrepancy_sequence
    public static float HaltonSequence(int indice, int b)//b = base
    {
        float resultado = 0f;
        float f = 1f / b;
        int i = indice;
        while (i > 0)
        {
            resultado = resultado + f * (i % b);
            i = Mathf.FloorToInt(i / b);
            f = f / b;
        }
        return resultado;
    }

    /// <summary>
    /// It Find the closest GameObject with respect to a point 
    /// </summary>
    /// <param name="gos"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static GameObject FindClosestGameObject(GameObject[] gos, Vector3 position)    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public static Renderer FindClosestRenderer(Renderer[] renderers, Vector3 position)
    {
        Renderer closest = null;
        float distance = Mathf.Infinity;
        foreach (Renderer render in renderers)
        {
            Vector3 diff = render.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = render;
                distance = curDistance;
            }
        }
        return closest;
    }

    /*
    entendiendo los layerMask:
    cualquier mask es un array de booleanos pero en bits, osea solo ceros y unos, en este caso un entero de 32 bits,
    ya que hay solo 32 layers que van desde el layer 0 hasta el layer 31, graficamente seria un superbyte de 32 digitos asi:
    0000 0000 0000 0000 0000 0000 0000 0000 donde el primer digito seria la ultima layer y el ultimo digito seria el primer layer
    por defecto unity contiene capas ya preestablecidas que serian estas a continuacion
                        0000 0000 0000 0000 0000 0000 1111 1111 layerMask.value
    default 0 =         0000 0000 0000 0000 0000 0000 0000 0001 = 1
    TransparentFx 1 =   0000 0000 0000 0000 0000 0000 0000 0010 = 2
    IgnoreRayCast 2 =   0000 0000 0000 0000 0000 0000 0000 0100 = 4
    --- 3 =             0000 0000 0000 0000 0000 0000 0000 1000 = 8          
    Water 4 =           0000 0000 0000 0000 0000 0000 0001 0000 = 16
    UI 5 =              0000 0000 0000 0000 0000 0000 0010 0000 = 32
    --- 6 =             0000 0000 0000 0000 0000 0000 0100 0000 = 64
    --- 7 =             0000 0000 0000 0000 0000 0000 1000 0000 = 128
    -------> de aqui en adelante hasta la capa 31 serian las capas que creamos

    un ejemplo de combinacion de capas seria asi por ejemplo:
    activar la capa 0,1y2 osea default + Transparent + IgnoreRaycast
                        0000 0000 0000 0000 0000 0000 0000 0111 = 7 = 1+2+4 
    ejemplo 2: activar la capa 4 y 5 osea Water y UI
                        0000 0000 0000 0000 0000 0000 0011 0000 = 48 = 16+32 

    en operaciones con bytes se usaria el operador << (operador de desplazamiento similar al insert en las teclas)
    1 << 4 (donde uno es el valor que se quiere reemplazar y 4 la posicion dentro del byte)
    si se desea combinar mas de una capa debe usarse el operador OR | de la siguiente manera
    1 << 4 | 1 << 5 (aqui estariamos combinando la capa 4 y 5)
    si se desea hacer la seleccion contraria se usaria el guion de la ñ (~) este invierte los valores del byte asi

    default 0 =         0000 0000 0000 0000 0000 0000 0000 0001 = 1
    invertir defalult = 1111 1111 1111 1111 1111 1111 1111 1110

    se usaria en codigo de la siguiente forma
    ~(1 << 1)

    una operacion contraria al desplazamiento aunque de manera muy extraña, seria el operador >>
    4 >> 1, donde 4 parece ser el layerMask.value y 1 la conversion a decimal, por alguna razon devolveria 1 seria algo asi:

   operadores de desplazamiento
Estos operadores tienen una prioridad más alta que la siguiente sección y menor precedencia que la sección anterior. 
NOTA, puede hacer clic en los titulares a que las páginas detalladas con ejemplos.
x << Y - bits de desplazamiento a la izquierda y se llenan de cero a la derecha.
x >> y - desplazamiento de bits de la derecha. Si el operando de la izquierda es de tipo int o largo, los bits luego a la izquierda se rellenan con el bit de signo. Si el operando de la izquierda es uint vs ulong, los bits que quedan luego se llenan de cero.
    http://answers.unity3d.com/questions/8715/how-do-i-use-layermasks.html

    */


}
