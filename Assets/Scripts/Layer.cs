public class Layer
{
    int size;
    public float[] neurons;
    public float[] weights;
    public Layer(int size)
    {
        if (size < 1) size = 1;
        this.size = size;
        neurons = new float[size];
    }
    public Layer(int size, int weightsCount)
    {
        this.size = size;
        neurons = new float[size];
        weights = new float[weightsCount];
    }
}
