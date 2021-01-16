#ifndef POI_MACROS
    #define POI_MACROS
    
    #define POI_TEXTURE_NOSAMPLER(tex) Texture2D tex; float4 tex##_ST; float2 tex##Pan; uint tex##UV

    #define POI2D_SAMPLER_PAN(tex, texSampler, uv, pan) (UNITY_SAMPLE_TEX2D_SAMPLER(tex, texSampler, TRANSFORM_TEX(uv, tex) + _Time.x * pan))
    #define POI2D_SAMPLER(tex, texSampler, uv) (UNITY_SAMPLE_TEX2D_SAMPLER(tex, texSampler, TRANSFORM_TEX(uv, tex)))
    #define POI2D_PAN(tex, uv, pan) (tex2D(tex, TRANSFORM_TEX(uv, tex) + _Time.x * pan))
    #define POI2D(tex, uv) (tex2D(tex, TRANSFORM_TEX(uv, tex)))
    
#endif